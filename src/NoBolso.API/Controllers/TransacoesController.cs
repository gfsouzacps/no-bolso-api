using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoBolso.Application.Commands.Transacoes; 
using NoBolso.Application.Queries.Transacoes;
using NoBolso.Domain.Enums;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ISender _mediator;

    public TransacoesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ListarTransacoesQueryResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar([FromQuery] ListarTransacoesQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ObterTransacaoPorIdQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var query = new ObterTransacaoPorIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Criar([FromBody] CriarTransacaoCommand command, CancellationToken cancellationToken)
    {
        var transacaoId = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(ObterPorId), new { id = transacaoId }, new { id = transacaoId });
    }

    public record AtualizarTransacaoRequest(
        string Descricao,
        decimal Valor,
        TipoTransacao TipoTransacao,
        DateTime DataTransacao
    );

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarTransacaoRequest request, CancellationToken cancellationToken)
    {
        var command = new AtualizarTransacaoCommand(
            id,
            request.Descricao,
            request.Valor,
            request.TipoTransacao,
            request.DataTransacao,
            Guid.Empty
        );
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remover(Guid id, CancellationToken cancellationToken)
    {
        var command = new RemoverTransacaoCommand(id, Guid.Empty);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}