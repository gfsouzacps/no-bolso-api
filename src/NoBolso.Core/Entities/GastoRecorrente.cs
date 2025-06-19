using NoBolso.Domain.Entities.Common;
using NoBolso.Domain.Enums;
using System;

namespace NoBolso.Domain.Entities;

public class GastoRecorrente : BaseEntity
{
    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }
    public TipoTransacao Tipo { get; private set; }
    public string Categoria { get; private set; } //TODO: Implementar por ENUM. Por enquanto uma string simples
    public FrequenciaGasto Frequencia { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime? DataFim { get; private set; }
    public bool Ativo { get; private set; }

    public Guid CarteiraId { get; private set; }
    public Carteira Carteira { get; private set; }

    public Guid GrupoId { get; private set; }
    public Grupo Grupo { get; private set; }

    // Mantemos o UsuarioId para saber QUEM criou/é responsável por este gasto
    public Guid CriadoPorUsuarioId { get; private set; }
    public Usuario CriadoPorUsuario { get; private set; }

    protected GastoRecorrente() { }

    public GastoRecorrente(
        string descricao,
        decimal valor,
        TipoTransacao tipo,
        string categoria,
        FrequenciaGasto frequencia,
        DateTime dataInicio,
        DateTime? dataFim,
        Guid carteiraId,
        Guid grupoId,
        Guid criadoPorUsuarioId)
    {
        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;
        Categoria = categoria;
        Frequencia = frequencia;
        DataInicio = dataInicio;
        DataFim = dataFim;
        CarteiraId = carteiraId;
        GrupoId = grupoId;
        CriadoPorUsuarioId = criadoPorUsuarioId;
        Ativo = true;
    }

    public void Desativar()
    {
        Ativo = false;
        SetAtualizadoEm();
    }
}
