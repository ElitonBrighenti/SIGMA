using Microsoft.EntityFrameworkCore;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repositories;
using Sigma.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SigmaContext _context;

        public UsuarioRepository(SigmaContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObterPorLoginAsync(string username, string password)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }

}
