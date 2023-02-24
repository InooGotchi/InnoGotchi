namespace InnoGotchi.Infrastructure.Identity.Services.Configurations;
public sealed record JwtTokenOptions
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int Expiration { get; set; }
}