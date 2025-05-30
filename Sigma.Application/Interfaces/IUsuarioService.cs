using Sigma.Application.Dtos;
using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task CadastrarUsuarioAsync(UsuarioCadastroDto dto);
        Task<IEnumerable<Usuario>> ListarTodosAsync();
        Task RemoverAsync(long id);
    }
}
