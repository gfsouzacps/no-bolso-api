using System;
using System.Collections.Generic;
using MediatR;
using NoBolso.Application.DTOs;
using NoBolso.Domain.Enums;

namespace NoBolso.Application.Queries.Transacoes
{
    public class ListarTransacoesQuery : IRequest<IEnumerable<TransacaoDto>>
    {
        public Guid? CarteiraId { get; set; }
        public TipoTransacao? TipoTransacao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}