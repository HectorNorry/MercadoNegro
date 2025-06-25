using MercadoNegro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoNegro.Core.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> RegistrarUsuario(UsuarioRegistroDTO registroDto);
        Task<Usuario> AutenticarUsuario(string email, string contraseña);
        Task<Usuario> GetUsuarioById(int id);
    }
}
