using InnoGotchi.Application.Common.Models;
using InnoGotchi.Domain.Common;

namespace InnoGotchi.Application.Common.Interfaces;

public interface IPetService : IService<PetViewModel, CreateUpdatePetModel, Guid>
{
    Task FeedPetAsync(Guid id);
}