namespace InnoGotchi.Application.Common.Interfaces;

public interface IService<TEntity, TCreateUpdateEntity, TId>
{
    Task<TEntity> GetByIdAsync(TId id);
    Task<IList<TEntity>> GetAllAsync();
    Task<TEntity> InsertAsync(TCreateUpdateEntity entity);
    Task<TEntity> UpdateAsync(TCreateUpdateEntity entity);
    Task DeleteAsync(TId id);
}