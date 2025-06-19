using Microsoft.EntityFrameworkCore;
using NoBolso.Domain.Entities.Common;
using NoBolso.Domain.Interfaces;
using NoBolso.Infrastructure.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NoBolso.Infrastructure.Repositories;
public class EfRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly NoBolsoDbContext _context;

    public EfRepository(NoBolsoDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> GetQueryable()
    {
        return _context.Set<T>().AsQueryable();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}