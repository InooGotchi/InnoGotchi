using InnoGotchi.Domain.Common.Base;

namespace InnoGotchi.Domain.Common;

public sealed class Farm : BaseEntity
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public int TotalPets { get; set; }
    public int AlivePets { get; set; }
    public int DeadPets { get; set; }
    public Player Owner { get; set; }
    public IEnumerable<Player> Players { get; set; }
    public IEnumerable<Pet> Pets { get; set; }
}