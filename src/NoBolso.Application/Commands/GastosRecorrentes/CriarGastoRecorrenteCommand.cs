using MediatR;
using NoBolso.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoBolso.Application.Commands.GastosRecorrentes;

public record CriarGastoRecorrenteCommand(
    string Descricao,
    decimal Valor,
    TipoTransacao Tipo,
    string Categoria,
    FrequenciaGasto Frequencia,
    DateTime DataInicio,
    DateTime? DataFim,
    Guid CarteiraId,
    Guid UsuarioLogadoId
) : IRequest<Guid>;