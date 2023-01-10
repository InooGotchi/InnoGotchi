namespace InnoGotchi.Domain.Common;

public class Pet: BaseEntity
{
    public string Name { get; set; }
    public PetType Type { get; set; }
    public PetBody Body{ get; set; }
    public int Age { get; set; }
    public int Hunger { get; set; } // 0-3
    public int Thurst { get; set; } // 0-3
    public int Happiness { get; set; }
    
    public Farm Farm { get; set; }
}
