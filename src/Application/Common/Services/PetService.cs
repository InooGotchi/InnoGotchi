using InnoGotchi.Application.Common.Interfaces;
using InnoGotchi.Domain.Common;

namespace Microsoft.Extensions.DependencyInjection.Common.Services;

public class PetService : IPetService
{
    private readonly IRepository<Pet> _repository;

    public PetService(IRepository<Pet> repository)
    {
        _repository = repository;
    }
    
    public Task<Pet> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Pet>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Pet> InsertAsync(Pet entity)
    {
        throw new NotImplementedException();
    }

    public Task<Pet> UpdateAsync(Pet entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task FeedPetAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}