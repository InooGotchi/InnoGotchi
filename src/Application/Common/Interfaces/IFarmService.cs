using InnoGotchi.Domain.Common;

namespace InnoGotchi.Application.Common.Interfaces;

public interface IFarmService : IService<Farm, Guid>
{
    Task AddPetAsync(Pet pet);
    Task RemovePetAsync(Guid id);
}