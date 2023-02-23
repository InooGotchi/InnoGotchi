namespace Web.Models;
public sealed record AuthenticationResponseDTO
{
    public Guid? UserId { get; set; }
    public string? Name { get; set; }
    public string? Token { get; set; }
}