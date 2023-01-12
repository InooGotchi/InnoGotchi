namespace InnoGotchi.Domain.Common;

public class Pet: BaseEntity
{
    public string Name { get; set; }
    public PetBody Body{ get; set; }
    public int Age { get; set; }
    public int HungerEnum { get; set; }
    public int ThurstEnum { get; set; }
    public int Happiness { get; set; }
    
    public Farm Farm { get; set; }
}
