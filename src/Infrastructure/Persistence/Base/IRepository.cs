using System.Linq.Expressions;
using InnoGotchi.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace InnoGotchi.Infrastructure.Persistence.Base;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IList<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true);

    Task<TEntity?> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true);
    
    ValueTask<EntityEntry<TEntity>> InsertAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);
    
    Task<EntityEntry<TEntity>> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);
    
    Task DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);
}
