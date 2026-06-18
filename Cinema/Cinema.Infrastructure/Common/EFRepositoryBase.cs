using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Cinema.Infrastructure.Common;

public class EFRepositoryBase<TEntity, TContext>(TContext context)
    : IRepository<TEntity>
    where TEntity : EntityBase
    where TContext : DbContext
{
    public IQueryable<TEntity> Query() => context.Set<TEntity>();

    public async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>> include = default)
    {
        IQueryable<TEntity> query = Query().AsNoTracking();
        if (include != null)
            query = include(query);
        return await query.SingleOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>> include = default)
    {
        IQueryable<TEntity> query = Query().AsNoTracking();
        if (include != null)
            query = include(query);
        if (predicate != null)
            query = query.Where(predicate);
        return await query.ToListAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        IQueryable<TEntity> query = Query();
        if (predicate != null)
            query = query.Where(predicate);
        return await query.AnyAsync();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await context.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        entity.ModifiedAt = DateTime.Now;
        context.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        entity.DeletedAt = DateTime.Now;
        entity.IsActive = false;
        context.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}
