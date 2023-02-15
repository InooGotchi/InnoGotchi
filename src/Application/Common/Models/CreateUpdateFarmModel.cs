namespace InnoGotchi.Application.Common.Models;

public sealed class CreateUpdateFarmModel
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public int TotalPets { get; set; }
    public int AlivePets { get; set; }
    public int DeadPets { get; set; }
    public UserModel Owner { get; set; }
    public IEnumerable<UserModel> Colaborators { get; set; }
}