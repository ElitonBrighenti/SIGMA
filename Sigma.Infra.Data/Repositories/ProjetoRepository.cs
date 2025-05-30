using Microsoft.EntityFrameworkCore;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repositories;
using Sigma.Infra.Data.Context;

namespace Sigma.Infra.Data.Repositories
{
    public class ProjetoRepository : IProjetoRepository
    {
        private readonly SigmaContext _dbContext;

        public ProjetoRepository(SigmaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Inserir(Projeto entidade)
        {
           await _dbContext.Set<Projeto>().AddAsync(entidade);
           await _dbContext.SaveChangesAsync();
           return true;
        }

        public async Task<List<Projeto>> BuscarTodos()
        {
            return await _dbContext.Projetos.ToListAsync();
        }

        public async Task<Projeto> ObterPorIdAsync(long id)
        {
            return await _dbContext.Projetos.FindAsync(id);
        }

        public async Task ExcluirAsync(Projeto projeto)
        {
            _dbContext.Projetos.Remove(projeto);
            await _dbContext.SaveChangesAsync();
        }
        public async Task AtualizarAsync(Projeto projeto)
        {
            _dbContext.Projetos.Update(projeto);
            await _dbContext.SaveChangesAsync();
        }

    }
}
