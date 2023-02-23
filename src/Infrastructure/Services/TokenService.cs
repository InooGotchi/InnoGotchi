using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InnoGotchi.Infrastructure.Services;
public sealed class TokenService : ITokenService
{
    private readonly JwtTokenOptions _jwtConfiguration;
    public TokenService(IOptions<JwtTokenOptions> jwtConfiguration)
    {
        _jwtConfiguration = jwtConfiguration.Value;
    }

    public string GenerateJWTToken((Guid? userId, string? userName) userDetails)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var (userId, userName) = userDetails;

        var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, userId.ToString()),
                new Claim("UserId", userId.ToString())
            };

        var token = new JwtSecurityToken(
            issuer: _jwtConfiguration.Issuer,
            audience: _jwtConfiguration.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.Expiration)),
            signingCredentials: signingCredentials
       );

        var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return encodedToken;
    }

    public sealed class JwtTokenOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Expiration { get; set; }
    }
}
