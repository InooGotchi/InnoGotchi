using InnoGotchi.Domain.Common.Base;

namespace InnoGotchi.Domain.Common;

public class Farm : BaseEntity
{
    public string Name { get; set; } = default!;
    public int Capacity { get; set; }
    public int TotalPets { get; set; }
    public int AlivePets { get; set; }
    public int DeadPets { get; set; }
    public User Owner { get; set; } = default!;
    public IEnumerable<User> Colaborators { get; set; } = default!;
    public IEnumerable<Pet> Pets { get; set; } = default!;
}
