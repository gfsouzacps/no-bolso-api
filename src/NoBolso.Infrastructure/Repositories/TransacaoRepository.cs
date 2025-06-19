using Microsoft.EntityFrameworkCore;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Enums;
using NoBolso.Domain.Interfaces.Repositories;
using NoBolso.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoBolso.Infrastructure.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly NoBolsoDbContext _context;

        public TransacaoRepository(NoBolsoDbContext context)
        {
            _context = context;
        }

        public async Task<Transacao> ObterPorIdAsync(Guid id)
        {
            return await _context.Transacoes
                .Include(t => t.Carteira)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Transacao>> ListarComFiltrosAsync(
            Guid? carteiraId,
            TipoTransacao? tipoTransacao,
            DateTime? dataInicio,
            DateTime? dataFim,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Transacoes.AsQueryable();

            if (carteiraId.HasValue)
            {
                query = query.Where(t => t.CarteiraId == carteiraId.Value);
            }

            if (tipoTransacao.HasValue)
            {
                query = query.Where(t => t.TipoTransacao == tipoTransacao.Value);
            }

            if (dataInicio.HasValue)
            {
                query = query.Where(t => t.DataTransacao >= dataInicio.Value);
            }

            if (dataFim.HasValue)
            {
                query = query.Where(t => t.DataTransacao <= dataFim.Value);
            }

            return await query.OrderByDescending(t => t.DataTransacao)
                              .Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public async Task<Transacao> AdicionarAsync(Transacao transacao)
        {
            await _context.Transacoes.AddAsync(transacao);
            await _context.SaveChangesAsync();
            return transacao;
        }

        public async Task AtualizarAsync(Transacao transacao)
        {
            _context.Transacoes.Update(transacao);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var transacao = await _context.Transacoes.FirstOrDefaultAsync(t => t.Id == id);
            if (transacao != null)
            {
                transacao.Desativar();
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(Guid id)
        {
            return await _context.Transacoes.AnyAsync(t => t.Id == id);
        }
    }
}