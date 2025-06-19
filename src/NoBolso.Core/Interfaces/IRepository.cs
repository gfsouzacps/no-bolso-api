using NoBolso.Domain.Entities.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NoBolso.Domain.Interfaces;
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Retorna um IQueryable para construir consultas customizadas.
    /// Esta é a base para as Queries do CQRS.
    /// </summary>
    IQueryable<T> GetQueryable();

    /// <summary>
    /// Busca uma entidade pelo seu ID.
    /// </summary>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adiciona uma nova entidade.
    /// </summary>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Marca uma entidade para ser atualizada.
    /// </summary>
    void Update(T entity);

    /// <summary>
    /// Marca uma entidade para ser removida.
    /// </summary>
    void Delete(T entity);

    /// <summary>
    /// Salva todas as mudanças pendentes no banco de dados. (Padrão Unit of Work)
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}