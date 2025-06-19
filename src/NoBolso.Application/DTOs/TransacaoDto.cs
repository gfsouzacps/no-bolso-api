using System;
using NoBolso.Domain.Enums;

namespace NoBolso.Application.DTOs
{
    public class TransacaoDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public string TipoTransacaoDescricao => TipoTransacao.ToString();
        public DateTime DataTransacao { get; set; }
        public Guid CarteiraId { get; set; }
        public string NomeCarteira { get; set; }
        public bool Ativo { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}