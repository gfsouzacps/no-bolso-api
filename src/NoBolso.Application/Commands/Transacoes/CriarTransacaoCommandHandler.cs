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
using NoBolso.Domain.Interfaces.Services;

namespace NoBolso.Application.Commands.Transacoes
{
    public class CriarTransacaoCommandHandler : IRequestHandler<CriarTransacaoCommand, TransacaoDto>
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly ICarteiraRepository _carteiraRepository;
        private readonly IEventService _eventService; // Injetar o serviço de eventos
        private readonly IMapper _mapper;

        public CriarTransacaoCommandHandler(
            ITransacaoRepository transacaoRepository,
            ICarteiraRepository carteiraRepository,
            IEventService eventService,
            IMapper mapper)
        {
            _transacaoRepository = transacaoRepository;
            _carteiraRepository = carteiraRepository;
            _eventService = eventService;
            _mapper = mapper;
        }

        public async Task<TransacaoDto> Handle(CriarTransacaoCommand request, CancellationToken cancellationToken)
        {
            var carteira = await _carteiraRepository.ObterPorIdAsync(request.CarteiraId);
            if (carteira == null)
                throw new InvalidOperationException($"Carteira com ID {request.CarteiraId} não foi encontrada.");

            var transacao = new Transacao(
                request.Descricao,
                request.Valor,
                request.TipoTransacao,
                request.DataTransacao,
                request.CarteiraId
            );

            var transacaoSalva = await _transacaoRepository.AdicionarAsync(transacao);

            // Publicar o evento aqui dentro!
            await _eventService.PublicarTransacaoCriadaAsync(new TransacaoCriadaEvent(transacaoSalva));

            var transacaoDto = _mapper.Map<TransacaoDto>(transacaoSalva);
            transacaoDto.NomeCarteira = carteira.Nome;

            return transacaoDto;
        }
    }
}