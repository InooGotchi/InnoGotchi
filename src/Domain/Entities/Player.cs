using InnoGotchi.Domain.Common.Base;

namespace InnoGotchi.Domain.Common;

public sealed class Player : BaseEntity
{
    public Guid ApplicationUserId { get; set; }
    public string ImagePath { get; set; }
    public Farm Farm { get; set; }
    public IEnumerable<Farm> Farms { get; set; }
}
