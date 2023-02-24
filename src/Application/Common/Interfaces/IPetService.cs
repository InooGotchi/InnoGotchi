using InnoGotchi.Application.Common.Models;

namespace InnoGotchi.Application.Common.Interfaces;

public interface IPetService : IService<PetViewModel, CreateUpdatePetModel, Guid>
{
    Task<int> UpdateAlivePetsStatuses();
    Task<PetViewModel> FeedPetAsync(Guid id);
    Task<PetViewModel> DrinkPetAsync(Guid id);
}