using InnoGotchi.Domain.Common.Base;

namespace InnoGotchi.Domain.Common;

public sealed class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string ImagePath { get; set; }
    public string PWHash{ get; set; }
    public string Token{ get; set; }
    
    public IEnumerable<Farm> Farms { get; set; }
}
