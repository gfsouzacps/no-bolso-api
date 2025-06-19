using MediatR;
using NoBolso.Application.Commands.Transacoes;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Events;
using NoBolso.Domain.Interfaces;
using NoBolso.Domain.Interfaces.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NoBolso.Application.Commands.Transacoes;

public class CriarTransacaoCommandHandler : IRequestHandler<CriarTransacaoCommand, Guid>
{
    private readonly IRepository<Transacao> _transacaoRepository;
    private readonly IRepository<Carteira> _carteiraRepository;
    private readonly IEventService _eventService;

    public CriarTransacaoCommandHandler(
        IRepository<Transacao> transacaoRepository,
        IRepository<Carteira> carteiraRepository,
        IEventService eventService)
    {
        _transacaoRepository = transacaoRepository;
        _carteiraRepository = carteiraRepository;
        _eventService = eventService;
    }

    public async Task<Guid> Handle(CriarTransacaoCommand request, CancellationToken cancellationToken)
    {
        // 1. Validação: Checar se a carteira existe.
        var carteira = await _carteiraRepository.GetByIdAsync(request.CarteiraId, cancellationToken)
            ?? throw new InvalidOperationException($"Carteira com ID {request.CarteiraId} não foi encontrada.");

        // Futuramente, a validação de permissão será mais robusta,
        // checando se o request.UsuarioLogadoId pertence ao carteira.Grupo.

        // 2. Criação da Entidade
        var transacao = new Transacao(
            request.Descricao,
            request.Valor,
            request.TipoTransacao,
            request.DataTransacao,
            request.CarteiraId,
            request.UsuarioLogadoId // Passando o ID de quem criou a transação
        );

        // 3. Persistência
        await _transacaoRepository.AddAsync(transacao, cancellationToken);
        await _transacaoRepository.SaveChangesAsync(cancellationToken);

        // 4. Publicação do Evento (APÓS salvar no banco)
        // await _eventService.PublicarTransacaoCriadaAsync(new TransacaoCriadaEvent(transacao)); // Reativar se necessário

        // 5. Retorno do ID
        return transacao.Id;
    }
}