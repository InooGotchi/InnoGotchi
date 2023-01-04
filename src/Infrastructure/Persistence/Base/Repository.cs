using InnoGotchi.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Infrastructure.Persistence.Base;
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entities;
    public Repository(ApplicationDbContext context)
    {
        this._context = context;
        _entities = context.Set<T>();
    }
    public IEnumerable<T> GetAll() => _entities.AsEnumerable();

    public T? Get(Guid id) => _entities.SingleOrDefault(s => s.Id == id);

    public void Insert(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"Cannot insert {entity}");
        }
        _entities.Add(entity);
        _context.SaveChanges();
    }
    public void Update(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"Cannot update {entity}");
        }
        _context.SaveChanges();
    }
    public void Delete(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"Cannot delete {entity}");
        }
        _entities.Remove(entity);
        _context.SaveChanges();
    }
}
