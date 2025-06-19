using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Enums;

namespace NoBolso.Domain.Interfaces.Repositories
{
    public interface ITransacaoRepository
    {
        Task<Transacao> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Transacao>> ListarComFiltrosAsync(
                    Guid? carteiraId,
                    TipoTransacao? tipoTransacao,
                    DateTime? dataInicio,
                    DateTime? dataFim,
                    int pageNumber,
                    int pageSize);
        Task<Transacao> AdicionarAsync(Transacao transacao);
        Task AtualizarAsync(Transacao transacao);
        Task RemoverAsync(Guid id);
        Task<bool> ExisteAsync(Guid id);
    }
}