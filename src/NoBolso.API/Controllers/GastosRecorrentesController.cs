using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoBolso.Application.Commands.GastosRecorrentes;
using NoBolso.Application.Queries.GastosRecorrentes;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
// [Authorize] // Adicionaremos isso quando o login estiver pronto
public class GastosRecorrentesController : ControllerBase
{
    private readonly ISender _mediator;

    public GastosRecorrentesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarGastoRecorrenteCommand command)
    {
        // NOTA: No futuro, o UsuarioLogadoId virá do token JWT, não do corpo da requisição.
        var gastoId = await _mediator.Send(command);
        return CreatedAtAction(nameof(ObterPorId), new { id = gastoId }, new { id = gastoId });
    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] ListarGastosRecorrentesQuery query)
    {
        // NOTA: O GrupoId será obtido a partir do grupo ativo do usuário logado.
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // Endpoint de exemplo para o CreatedAtAction funcionar
    [HttpGet("{id:guid}")]
    public IActionResult ObterPorId(Guid id)
    {
        return Ok(new { Message = "Endpoint de obter por ID a ser implementado.", Id = id });
    }
}
