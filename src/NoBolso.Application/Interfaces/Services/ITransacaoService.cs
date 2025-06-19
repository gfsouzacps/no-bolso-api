using MediatR;
using NoBolso.Domain.Enums;

namespace NoBolso.Application.Commands;

public class ListarTransacoesQuery : IRequest<Guid>
{
    public string Descricao { get; init; }
    public decimal Valor { get; init; }
    public TipoTransacao Tipo { get; init; }
    public DateTime Data { get; init; }
    public Guid CarteiraId { get; init; }

    public ListarTransacoesQuery(string descricao, decimal valor, TipoTransacao tipo, DateTime data, Guid carteiraId)
    {
        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;
        Data = data;
        CarteiraId = carteiraId;
    }
}