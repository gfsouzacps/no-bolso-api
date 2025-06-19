using AutoMapper;
using MediatR;
using NoBolso.Application.Commands.Transacoes;
using NoBolso.Application.DTOs;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Events;
using NoBolso.Domain.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NoBolso.Application.Commands.Transacoes
{
    public class AtualizarTransacaoCommandHandler : IRequestHandler<AtualizarTransacaoCommand, TransacaoDto>
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IMapper _mapper;

        public AtualizarTransacaoCommandHandler(ITransacaoRepository transacaoRepository, IMapper mapper)
        {
            _transacaoRepository = transacaoRepository;
            _mapper = mapper;
        }

        public async Task<TransacaoDto> Handle(AtualizarTransacaoCommand request, CancellationToken cancellationToken)
        {
            var transacao = await _transacaoRepository.ObterPorIdAsync(request.Id);
            if (transacao == null)
            {
                return null; // Ou lançar exceção
            }

            transacao.AtualizarDescricao(request.Descricao);
            transacao.AtualizarValor(request.Valor);
            transacao.AtualizarTipoTransacao(request.TipoTransacao);
            transacao.AtualizarDataTransacao(request.DataTransacao);

            await _transacaoRepository.AtualizarAsync(transacao);

            return _mapper.Map<TransacaoDto>(transacao);
        }
    }
}