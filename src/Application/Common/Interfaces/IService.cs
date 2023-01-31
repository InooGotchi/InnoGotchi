namespace InnoGotchi.Application.Common.Interfaces;

public interface IService<TEntity, TId>
{
    Task<TEntity> GetByIdAsync(TId id);
    Task<IList<TEntity>> GetAllAsync();
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(TId id);
}