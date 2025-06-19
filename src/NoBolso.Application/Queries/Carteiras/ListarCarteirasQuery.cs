using MediatR;
using NoBolso.Application.DTOs;
using System.Collections.Generic;

namespace NoBolso.Application.Queries.Carteiras
{
    public class ListarCarteirasQuery : IRequest<IEnumerable<CarteiraDto>>
    {
    }
}