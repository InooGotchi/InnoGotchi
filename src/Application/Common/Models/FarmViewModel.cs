using InnoGotchi.Application.Common.Models.Base;

namespace InnoGotchi.Application.Common.Models;

public sealed class FarmViewModel : BaseModel
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public int TotalPets { get; set; }
    public int AlivePets { get; set; }
    public int DeadPets { get; set; }
    public UserModel Owner { get; set; }
    public IEnumerable<UserModel> Colaborators { get; set; }
    public IEnumerable<PetViewModel> Pets { get; set; }
}