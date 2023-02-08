namespace InnoGotchi.Domain.Common.Base;

public abstract class BodyPart : BaseEntity
{
    public string Name { get; init; }
    public string Url { get; init; }
    
    public IEnumerable<PetBody> PetBodies { get; set; }
}