using InnoGotchi.Application.Common.Interfaces;
using InnoGotchi.Domain.Common;

namespace Microsoft.Extensions.DependencyInjection.Common.Services;

public class FarmService : IFarmService
{
    private readonly IRepository<Farm> _repository;
    private readonly IPetService _petService;

    public FarmService(IRepository<Farm> repository, IPetService petService)
    {
        _repository = repository;
        _petService = petService;
    }
    
    public Task<Farm> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Farm>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Farm> InsertAsync(Farm entity)
    {
        throw new NotImplementedException();
    }

    public Task<Farm> UpdateAsync(Farm entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task AddPetAsync(Pet pet)
    {
        throw new NotImplementedException();
    }

    public Task RemovePetAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}