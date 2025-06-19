using MediatR;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Enums;
using NoBolso.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoBolso.Application.Commands.GastosRecorrentes;

public class CriarGastoRecorrenteCommandHandler : IRequestHandler<CriarGastoRecorrenteCommand, Guid>
{
    private readonly IRepository<GastoRecorrente> _gastoRepository;
    private readonly IRepository<Carteira> _carteiraRepository;

    public CriarGastoRecorrenteCommandHandler(IRepository<GastoRecorrente> gastoRepository, IRepository<Carteira> carteiraRepository)
    {
        _gastoRepository = gastoRepository;
        _carteiraRepository = carteiraRepository;
    }

    public async Task<Guid> Handle(CriarGastoRecorrenteCommand request, CancellationToken cancellationToken)
    {
        // 1. Encontrar a carteira para obter o GrupoId
        var carteira = await _carteiraRepository.GetByIdAsync(request.CarteiraId, cancellationToken)
            ?? throw new Exception("Carteira não encontrada.");

        // TODO: Futuramente, validar se o UsuarioLogadoId pertence ao carteira.GrupoId

        // 2. Criar a nova entidade
        var novoGasto = new GastoRecorrente(
            request.Descricao,
            request.Valor,
            request.Tipo,
            request.Categoria,
            request.Frequencia,
            request.DataInicio,
            request.DataFim,
            request.CarteiraId,
            carteira.GrupoId,      // O GrupoId é herdado da carteira
            request.UsuarioLogadoId
        );

        // 3. Salvar no banco
        await _gastoRepository.AddAsync(novoGasto, cancellationToken);
        await _gastoRepository.SaveChangesAsync(cancellationToken);

        return novoGasto.Id;
    }
}