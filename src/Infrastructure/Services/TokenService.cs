using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace InnoGotchi.Infrastructure.Services;
public sealed class TokenService : ITokenService
{
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly string _expiryMinutes;

    public TokenService(string key, string issueer, string audience, string expiryMinutes)
    {
        _key = key;
        _issuer = issueer;
        _audience = audience;
        _expiryMinutes = expiryMinutes;
    }

    public string GenerateJWTToken((Guid? userId, string? userName) userDetails)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var (userId, userName) = userDetails;

        var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, userId.ToString()),
                new Claim(ClaimTypes.Name, userName),
                new Claim("UserId", userId.ToString())
            };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_expiryMinutes)),
            signingCredentials: signingCredentials
       );

        var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return encodedToken;
    }
}
