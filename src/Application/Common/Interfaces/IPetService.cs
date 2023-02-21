using InnoGotchi.Application.Common.Models;
using InnoGotchi.Domain.Common;

namespace InnoGotchi.Application.Common.Interfaces;

public interface IPetService : IService<PetViewModel, CreateUpdatePetModel, Guid>
{
    Task<int> UpdateAlivePetsStatuses();
    Task<PetViewModel> FeedPetAsync(Guid id);
    Task<PetViewModel> HydratePetAsync(Guid id);
}