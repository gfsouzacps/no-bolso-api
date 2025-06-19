using System;
using MediatR;
using NoBolso.Application.DTOs;
using NoBolso.Domain.Enums;

namespace NoBolso.Application.Commands.Transacoes
{
    public class CriarTransacaoCommand : IRequest<TransacaoDto>
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public DateTime DataTransacao { get; set; }
        public Guid CarteiraId { get; set; }
    }
}