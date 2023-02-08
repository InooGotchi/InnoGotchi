using InnoGotchi.Application.Common.Models.Base;

namespace InnoGotchi.Application.Common.Models;

public sealed class PetViewModel : BaseModel
{
    public string Name { get; set; }
    public PetBodyModel Body{ get; set; }
    public int Age { get; set; }
    public int HungerEnum { get; set; }
    public int ThurstEnum { get; set; }
    public int Happiness { get; set; }
}