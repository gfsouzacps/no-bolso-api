using MediatR;
using Microsoft.EntityFrameworkCore;
using NoBolso.Application.Commands.Transacoes;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Events;
using NoBolso.Domain.Interfaces;
using NoBolso.Domain.Interfaces.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NoBolso.Application.Commands.Transacoes;

public class RemoverTransacaoCommandHandler : IRequestHandler<RemoverTransacaoCommand, Unit>
{
    private readonly IRepository<Transacao> _repository;

    public RemoverTransacaoCommandHandler(IRepository<Transacao> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(RemoverTransacaoCommand request, CancellationToken cancellationToken)
    {
        var transacao = await _repository.GetQueryable()
            .Include(t => t.Carteira)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        // MUDANÇA: A checagem de permissão compara o GrupoId da carteira com o do usuário.
        if (transacao == null || transacao.Carteira.GrupoId != request.GrupoIdDoUsuario)
        {
            throw new Exception($"Transação com ID {request.Id} não encontrada ou permissão negada.");
        }

        _repository.Delete(transacao);
        await _repository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}