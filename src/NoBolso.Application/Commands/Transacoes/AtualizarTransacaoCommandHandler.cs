using MediatR;
using Microsoft.EntityFrameworkCore;
using NoBolso.Application.Commands.Transacoes;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Events;
using NoBolso.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NoBolso.Application.Commands.Transacoes;

public class AtualizarTransacaoCommandHandler : IRequestHandler<AtualizarTransacaoCommand, Unit>
{
    private readonly IRepository<Transacao> _repository;

    public AtualizarTransacaoCommandHandler(IRepository<Transacao> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(AtualizarTransacaoCommand request, CancellationToken cancellationToken)
    {
        var transacao = await _repository.GetQueryable()
            .Include(t => t.Carteira)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (transacao == null || transacao.Carteira.UsuarioId != request.UsuarioId)
        {
            throw new Exception($"Transação com ID {request.Id} não encontrada ou permissão negada.");
        }

        transacao.AtualizarDescricao(request.Descricao);
        transacao.AtualizarValor(request.Valor);
        transacao.AtualizarTipoTransacao(request.TipoTransacao);
        transacao.AtualizarDataTransacao(request.DataTransacao);

        await _repository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}