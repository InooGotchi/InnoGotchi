namespace CleanArchitecture.Application.Common.Interfaces;
public interface ITokenService
{
    public string GenerateJWTToken((Guid? userId, string? userName) userDetails);
}
