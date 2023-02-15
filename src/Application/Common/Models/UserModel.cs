using InnoGotchi.Application.Common.Models.Base;

namespace InnoGotchi.Application.Common.Models;

public sealed class UserModel : BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string ImagePath { get; set; }
    public string PWHash{ get; set; }
    public string Token{ get; set; }
}