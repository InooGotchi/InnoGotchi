using InnoGotchi.Application.Common.Models;
using InnoGotchi.Domain.Common;

namespace InnoGotchi.Application.Common.Interfaces;

public interface IFarmService : IService<FarmViewModel, CreateUpdateFarmModel, Guid>
{
    Task AddPetAsync(CreateUpdatePetModel pet);
    Task RemovePetAsync(Guid id);
}