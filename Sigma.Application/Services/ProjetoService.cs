using AutoMapper;
using Sigma.Application.Dtos;
using Sigma.Application.Interfaces;
using Sigma.Domain.Dtos;
using Sigma.Domain.Entities;
using Sigma.Domain.Enums;
using Sigma.Domain.Interfaces.Repositories;

namespace Sigma.Application.Services
{
    public class ProjetoService : IProjetoService
    {
        private readonly IMapper _mapper;
        private readonly IProjetoRepository _projetoRepository;
        
        public ProjetoService(IMapper mapper, IProjetoRepository projetoRepository)
        {
            _mapper = mapper;
            _projetoRepository = projetoRepository;
        }

        public async Task<bool> Inserir(ProjetoNovoDto model)
        {
            return await _projetoRepository.Inserir(_mapper.Map<Projeto>(model));
        }
        public async Task<List<ProjetosDto>> BuscarTodos()
        {
            var projetos = await _projetoRepository.BuscarTodos();
            return _mapper.Map<List<ProjetosDto>>(projetos);
        }

        public async Task<List<ProjetosDto>> BuscarPorFiltro(string? nome, StatusProjeto? status)
        {
            var projetos = await _projetoRepository.BuscarTodos();

            if (!string.IsNullOrWhiteSpace(nome))
                projetos = projetos.Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();

            if (status.HasValue)
                projetos = projetos.Where(p => p.Status == status.Value).ToList();

            return _mapper.Map<List<ProjetosDto>>(projetos);
        }


        public async Task ExcluirProjetoAsync(long id)
        {
            var projeto = await _projetoRepository.ObterPorIdAsync(id);
            if (projeto == null)
                throw new Exception("Projeto não encontrado.");

            if (projeto.Status == StatusProjeto.Iniciado ||
                projeto.Status == StatusProjeto.Planejado ||
                projeto.Status == StatusProjeto.EmAndamento ||
                projeto.Status == StatusProjeto.Encerrado)
            {
                throw new Exception("Projetos neste status não podem ser excluídos.");
            }

            await _projetoRepository.ExcluirAsync(projeto);
        }

        //public async Task AtualizarProjetoAsync(int id, ProjetosDto dto)
        //{
        //    var projeto = await _projetoRepository.ObterPorIdAsync(id);
        //    if (projeto == null)
        //        throw new Exception("Projeto não encontrado");

        //    projeto.Nome = dto.Nome;
        //    projeto.Status = dto.Status;

        //    await _projetoRepository.AtualizarAsync(projeto);
        //}
        public async Task AtualizarProjetoAsync(int id, AtualizaStatusDTo dto)
        {
            var projeto = await _projetoRepository.ObterPorIdAsync(id);
            if (projeto == null)
                throw new Exception("Projeto não encontrado");

            projeto.Status = dto.Status;

            await _projetoRepository.AtualizarAsync(projeto);
        }

    }
}
