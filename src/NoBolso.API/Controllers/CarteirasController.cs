using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoBolso.Application.Commands.Carteiras;
using NoBolso.Application.Queries.Carteiras;


namespace NoBolso.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarteirasController : ControllerBase
{
    private readonly ISender _mediator;

    public CarteirasController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ListarCarteirasQueryResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar([FromQuery] ListarCarteirasQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ObterCarteiraQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var query = new ObterCarteiraQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Criar([FromBody] CriarCarteiraCommand command, CancellationToken cancellationToken)
    {
        var carteiraId = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(ObterPorId), new { id = carteiraId }, new { id = carteiraId });
    }

    public record AtualizarCarteiraRequest(string Nome);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarCarteiraRequest request, CancellationToken cancellationToken)
    {
        var command = new AtualizarCarteiraCommand(id, request.Nome);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remover(Guid id, CancellationToken cancellationToken)
    {
        var command = new RemoverCarteiraCommand(id);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}