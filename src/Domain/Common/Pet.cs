using InnoGotchi.Domain.Enums;

namespace InnoGotchi.Domain.Common;

public class Pet: BaseEntity
{
    public string Name { get; set; }
    //public PetBody Body{ get; set; }
    public int Age { get; set; }
    public DateTime NextFeedDate { get; set; }
    public DateTime NextDrinkDate { get; set; }
    public HungerEnum HungerEnum { get; set; }
    public ThirstEnum ThurstEnum { get; set; }
    public int Happiness { get; set; }
  
    public Farm Farm { get; set; }
}
