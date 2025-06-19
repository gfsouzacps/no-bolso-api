using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NoBolso.Application.DTOs;
using NoBolso.Application.Queries.Transacoes;
using NoBolso.Domain.Interfaces.Repositories;

namespace NoBolso.Application.Queries.Transacoes
{
    public class ListarTransacoesQueryHandler : IRequestHandler<ListarTransacoesQuery, IEnumerable<TransacaoDto>>
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly ICarteiraRepository _carteiraRepository;
        private readonly IMapper _mapper;

        public ListarTransacoesQueryHandler(
            ITransacaoRepository transacaoRepository,
            ICarteiraRepository carteiraRepository,
            IMapper mapper)
        {
            _transacaoRepository = transacaoRepository;
            _carteiraRepository = carteiraRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransacaoDto>> Handle(ListarTransacoesQuery request, CancellationToken cancellationToken)
        {
            // O repositório agora deve ser capaz de aplicar os filtros combinados.
            // Se nenhum filtro for passado, o repositório deve retornar todas as transações (sem filtros).
            var transacoes = await _transacaoRepository.ListarComFiltrosAsync(
                request.CarteiraId,
                request.TipoTransacao,
                request.DataInicio,
                request.DataFim,
                request.PageNumber,
                request.PageSize
            );

            // Aplicar paginação APÓS a busca no repositório, mas ANTES de carregar nomes de carteiras,
            // para que o Count reflita apenas os itens da página atual.
            var transacoesPaginadas = transacoes
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList(); // Materializa para evitar múltiplas enumerações

            var transacoesDto = _mapper.Map<IEnumerable<TransacaoDto>>(transacoesPaginadas).ToList(); // Materializa

            // Buscar nomes das carteiras para enriquecer os DTOs
            var carteiraIds = transacoesDto.Select(t => t.CarteiraId).Distinct().ToList();
            var carteiras = new Dictionary<Guid, string>();

            // Pode otimizar esta busca de carteiras buscando todas de uma vez, se possível
            // Ex: var todasCarteirasNecessarias = await _carteiraRepository.ObterPorIdsAsync(carteiraIds);
            // E então popular o dictionary.
            foreach (var carteiraId in carteiraIds)
            {
                var carteira = await _carteiraRepository.ObterPorIdAsync(carteiraId);
                if (carteira != null)
                {
                    carteiras[carteiraId] = carteira.Nome;
                }
            }

            // Enriquecer DTOs com nomes das carteiras
            foreach (var transacaoDto in transacoesDto)
            {
                if (carteiras.TryGetValue(transacaoDto.CarteiraId, out var nomeCarteira))
                {
                    transacaoDto.NomeCarteira = nomeCarteira;
                }
            }

            return transacoesDto;
        }
    }
}