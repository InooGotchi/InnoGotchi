using InnoGotchi.Domain.Common.Base;
using InnoGotchi.Domain.Common.BodyParts;

namespace InnoGotchi.Domain.Common;

public sealed class PetBody : BaseEntity
{
    public Guid PetId { get; set; }
    public Pet Pet { get; set; }
    
    
    public Guid NoseId { get; set; }
    public Nose Nose { get; init; }
    
    
    public Guid EyesId { get; set; }
    public Eyes Eyes { get; init; }
    
    
    public Guid MouthId { get; set; }
    public Mouth Mouth { get; init; }
    
    
    public Guid BodyId { get; set; }
    public Body Body { get; init; }
}