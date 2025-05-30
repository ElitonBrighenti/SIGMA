using Sigma.Application.Dtos;
using Sigma.Application.Interfaces;
using Sigma.Domain.Entities;
using Sigma.Domain.Interfaces.Repositories;
using Sigma.Infra.CrossCutting.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task CadastrarUsuarioAsync(UsuarioCadastroDto dto)
        {
            var existente = await _repo.ObterPorUsernameAsync(dto.Username);
            if (existente != null)
                throw new Exception("Usuário já existe.");

            var usuario = new Usuario
            {
                Username = dto.Username,
                Password = HashHelper.GerarHashMD5(dto.Password),
                Role = dto.Role
            };

            await _repo.AdicionarAsync(usuario);
        }
        public async Task<IEnumerable<Usuario>> ListarTodosAsync()
        {
            return await _repo.ListarTodosAsync();
        }

        public async Task RemoverAsync(long id)
        {
            await _repo.RemoverAsync(id);
        }
    }
}
