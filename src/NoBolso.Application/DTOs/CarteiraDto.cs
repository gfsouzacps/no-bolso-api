using System;
using System.Collections.Generic;

namespace NoBolso.Application.DTOs
{
    public class CarteiraDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Saldo { get; set; }
        public int QuantidadeTransacoes { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public List<TransacaoDto> Transacoes { get; set; } = new();
    }
}