using InnoGotchi.Domain.Common;

namespace InnoGotchi.Application.Common.Interfaces;

public interface IPetService : IService<Pet, Guid>
{
    Task FeedPetAsync(Guid id);
}