namespace Web.Models;

public class UserAuthorizationModel
{
    public string Name { get; set; } = default!;
    public string Password { get; set; } = default!;
    public bool IsPersistent { get; set; } = default!;
}
