using InnoGotchi.Application.Common.Models.Base;

namespace InnoGotchi.Application.Common.Models;

public sealed class UserModel : BaseModel
{
    public string ImagePath { get; set; } = default!;
}