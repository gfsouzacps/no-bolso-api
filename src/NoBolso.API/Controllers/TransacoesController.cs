using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoBolso.Application.Commands.Transacoes;
using NoBolso.Application.DTOs;
using NoBolso.Application.Queries.Transacoes;

namespace NoBolso.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacoesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransacoesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TransacaoDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var query = new ObterTransacaoPorIdQuery { Id = id };
            var transacao = await _mediator.Send(query);
            return transacao != null ? Ok(transacao) : NotFound();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TransacaoDto>), 200)]
        public async Task<IActionResult> Listar([FromQuery] ListarTransacoesQuery query)
        {
            var transacoes = await _mediator.Send(query);
            return Ok(transacoes);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TransacaoDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Criar([FromBody] CriarTransacaoCommand command)
        {
            var transacaoDto = await _mediator.Send(command);
            return CreatedAtAction(nameof(ObterPorId), new { id = transacaoDto.Id }, transacaoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TransacaoDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarTransacaoCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("O ID da rota deve ser o mesmo do corpo da requisição.");
            }

            var transacaoDto = await _mediator.Send(command);
            return Ok(transacaoDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Remover(Guid id)
        {
            var command = new RemoverTransacaoCommand { Id = id };
            var sucesso = await _mediator.Send(command);
            return sucesso ? NoContent() : NotFound();
        }
    }
}