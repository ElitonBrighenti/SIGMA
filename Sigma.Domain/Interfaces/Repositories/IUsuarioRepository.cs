using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorLoginAsync(string username, string passwordHash);
        Task<Usuario?> ObterPorUsernameAsync(string username);
        Task AdicionarAsync(Usuario usuario);
        Task<IEnumerable<Usuario>> ListarTodosAsync();
        Task RemoverAsync(long id);
    }

}
