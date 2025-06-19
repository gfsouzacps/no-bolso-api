using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NoBolso.Domain.Entities;

namespace NoBolso.Domain.Interfaces.Repositories
{
    public interface ICarteiraRepository
    {
        Task<Carteira> ObterPorIdAsync(Guid id);
        Task<Carteira> ObterPorIdComTransacoesAsync(Guid id);
        Task<IEnumerable<Carteira>> ObterTodasAsync();
        Task<Carteira> AdicionarAsync(Carteira carteira);
        Task AtualizarAsync(Carteira carteira);
        Task RemoverAsync(Guid id);
        Task<bool> ExisteAsync(Guid id);
    }
}