using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanArchitecture.Application.Common.Interfaces;
using InnoGotchi.Infrastructure.Identity.Services.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InnoGotchi.Infrastructure.Services;
public sealed partial class TokenService : ITokenService
{
    private readonly JwtTokenOptions _options;
    public TokenService(IOptions<JwtTokenOptions> options)
    {
        _options = options.Value;
    }

    public string GenerateJWTToken((Guid? userId, string? userName) userDetails)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var (userId, userName) = userDetails;

        var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, userId.ToString()),
                new Claim("UserId", userId.ToString())
            };

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_options.Expiration)),
            signingCredentials: signingCredentials
       );

        var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return encodedToken;
    }
}
