using AutoMapper;
using MediatR;
using NoBolso.Application.Commands.Transacoes;
using NoBolso.Application.DTOs;
using NoBolso.Domain.Interfaces.Services;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Events;
using NoBolso.Domain.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NoBolso.Application.Commands.Transacoes
{
    public class RemoverTransacaoCommandHandler : IRequestHandler<RemoverTransacaoCommand, bool>
    {
        private readonly ITransacaoRepository _transacaoRepository;

        public RemoverTransacaoCommandHandler(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }

        public async Task<bool> Handle(RemoverTransacaoCommand request, CancellationToken cancellationToken)
        {
            var transacaoExiste = await _transacaoRepository.ExisteAsync(request.Id);
            if (!transacaoExiste)
            {
                return false;
            }

            await _transacaoRepository.RemoverAsync(request.Id);
            return true;
        }
    }
}