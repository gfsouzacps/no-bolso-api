using System;
using NoBolso.Domain.Entities;

namespace NoBolso.Domain.Events
{
    public class TransacaoCriadaEvent
    {
        public Guid TransacaoId { get; }
        public Guid CarteiraId { get; }
        public decimal Valor { get; }
        public string TipoTransacao { get; }
        public DateTime DataEvento { get; }

        public TransacaoCriadaEvent(Transacao transacao)
        {
            TransacaoId = transacao.Id;
            CarteiraId = transacao.CarteiraId;
            Valor = transacao.Valor;
            TipoTransacao = transacao.TipoTransacao.ToString();
            DataEvento = DateTime.UtcNow;
        }
    }
}