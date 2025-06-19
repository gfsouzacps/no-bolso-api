using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using NoBolso.Application.DTOs;
using NoBolso.Application.Commands.Carteiras;
using NoBolso.Application.Queries.Carteiras;

namespace NoBolso.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarteirasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarteirasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CarteiraDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ObterPorId(Guid id, [FromQuery] bool incluirTransacoes = false)
        {
            var query = new ObterCarteiraQuery(id, incluirTransacoes);
            var carteira = await _mediator.Send(query);
            return carteira != null ? Ok(carteira) : NotFound();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CarteiraDto>), 200)]
        public async Task<IActionResult> ListarTodas()
        {
            var query = new ListarCarteirasQuery();
            var carteiras = await _mediator.Send(query);
            return Ok(carteiras);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CarteiraDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Criar([FromBody] CriarCarteiraCommand command)
        {
            var carteiraDto = await _mediator.Send(command);
            return CreatedAtAction(nameof(ObterPorId), new { id = carteiraDto.Id }, carteiraDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CarteiraDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarCarteiraCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("O ID da rota deve ser o mesmo do corpo da requisição.");
            }

            var carteiraDto = await _mediator.Send(command);
            return Ok(carteiraDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Remover(Guid id)
        {
            var command = new RemoverCarteiraCommand { Id = id };
            var sucesso = await _mediator.Send(command);
            return sucesso ? NoContent() : NotFound();
        }
    }
}