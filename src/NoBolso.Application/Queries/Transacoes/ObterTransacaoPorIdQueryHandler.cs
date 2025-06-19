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
    public class ObterTransacaoPorIdQueryHandler : IRequestHandler<ObterTransacaoPorIdQuery, TransacaoDto>
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IMapper _mapper;

        public ObterTransacaoPorIdQueryHandler(ITransacaoRepository transacaoRepository, IMapper mapper)
        {
            _transacaoRepository = transacaoRepository;
            _mapper = mapper;
        }

        public async Task<TransacaoDto> Handle(ObterTransacaoPorIdQuery request, CancellationToken cancellationToken)
        {
            var transacao = await _transacaoRepository.ObterPorIdAsync(request.Id);
            return _mapper.Map<TransacaoDto>(transacao);
        }
    }
}