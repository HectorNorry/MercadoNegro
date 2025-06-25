using MercadoNegro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoNegro.Core.Interfaces
{
    namespace MercadoNegro.Core.Interfaces
    {
        public interface IUsuarioRepository
        {
            Task<Usuario> GetByIdAsync(int id);
            Task<Usuario> GetByEmailAsync(string email);
            Task<Usuario> GetByCvuAsync(string cvu);
            Task AddAsync(Usuario usuario);
            Task UpdateAsync(Usuario usuario);
            Task<bool> ExistsByEmailAsync(string email);
            Task<bool> ExistsByCvuAsync(string cvu);
        }
    }
}
