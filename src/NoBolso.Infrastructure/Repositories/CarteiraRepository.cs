using Microsoft.EntityFrameworkCore;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Interfaces.Repositories;
using NoBolso.Infrastructure.Data;

namespace NoBolso.Infrastructure.Repositories
{
    public class CarteiraRepository : ICarteiraRepository
    {
        private readonly NoBolsoDbContext _context;

        public CarteiraRepository(NoBolsoDbContext context)
        {
            _context = context;
        }

        public async Task<Carteira> ObterPorIdAsync(Guid id)
        {
            return await _context.Carteiras
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Carteira> ObterPorIdComTransacoesAsync(Guid id)
        {
            return await _context.Carteiras
                .Include(c => c.Transacoes.Where(t => t.Ativo))
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Carteira>> ObterTodasAsync()
        {
            return await _context.Carteiras
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<Carteira> AdicionarAsync(Carteira carteira)
        {
            await _context.Carteiras.AddAsync(carteira);
            await _context.SaveChangesAsync();
            return carteira;
        }

        public async Task AtualizarAsync(Carteira carteira)
        {
            _context.Carteiras.Update(carteira);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)  //Inativa a carteira
        {
            var carteira = await _context.Carteiras
                                         .IgnoreQueryFilters()
                                         .FirstOrDefaultAsync(c => c.Id == id);

            if (carteira != null)
            {
                carteira.Desativar();
                _context.Carteiras.Update(carteira);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(Guid id)
        {
            return await _context.Carteiras.AnyAsync(c => c.Id == id);
        }
    }
}