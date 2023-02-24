namespace InnoGotchi.Application.Common.Models;

public sealed class CreateUpdatePetModel
{
    public Guid EntityId { get; set; }
    public string Name { get; set; }
    public Guid NoseId { get; set; }
    public Guid EyesId { get; set; }
    public Guid MouthId { get; set; }
    public Guid BodyId { get; set; }
    public int Age { get; set; }
    public int HungerEnum { get; set; }
    public int ThurstEnum { get; set; }
    public int Happiness { get; set; }
    
    public Guid FarmId { get; set; }
}