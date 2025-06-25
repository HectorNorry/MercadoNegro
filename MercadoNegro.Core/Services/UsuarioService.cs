using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MercadoNegro.Core.DTOs;
using MercadoNegro.Core.Entities;
using MercadoNegro.Core.Interfaces;
using MercadoNegro.Core.Interfaces.MercadoNegro.Core.Interfaces;

namespace MercadoNegro.Core.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> AutenticarUsuario(string email, string contraseña)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(email);
            if (usuario == null || usuario.Contraseña != contraseña) // En producción usar hash
                return null;

            return usuario;
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }

        public async Task<Usuario> RegistrarUsuario(UsuarioRegistroDTO registroDto)
        {
            if (await _usuarioRepository.ExistsByEmailAsync(registroDto.Email))
                throw new Exception("El email ya está registrado");

            var usuario = new Usuario
            {
                Nombre = registroDto.Nombre,
                Apellido = registroDto.Apellido,
                Email = registroDto.Email,
                Contraseña = registroDto.Contraseña, // Debería ser hash en producción
                Cvu = GenerarCvuUnico(),
                Saldo = 0
            };

            await _usuarioRepository.AddAsync(usuario);
            return usuario;
        }

        private string GenerarCvuUnico()
        {
            // Implementación para generar CVU único
            return "CVU" + Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
        }
    }

}
