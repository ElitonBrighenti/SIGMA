using Microsoft.EntityFrameworkCore;
using Sigma.Domain.Entities;

namespace Sigma.Domain.Interfaces.Repositories
{
    public interface IProjetoRepository
    {
        Task<bool> Inserir(Projeto entidade);

        Task<List<Projeto>> BuscarTodos();
        Task<Projeto> ObterPorIdAsync(long id);
        Task ExcluirAsync(Projeto projeto);
        Task AtualizarAsync(Projeto projeto);


    }
}
