using InnoGotchi.Domain.Common;

namespace InnoGotchi.Application.Common.Interfaces;
public interface IIdentityService
{
    Task<bool> SignIn(string userName, string password, bool isPersistent);
    Task<(Result Result, Guid UserId)> CreateUserAsync(string userName, string password);
    Task<Result> DeleteUserAsync(Guid userId);
    Task<(Guid? id, string? userName)> GetUserDetailsAsync(Guid userId);
    Task<(Guid? id, string? userName)> GetUserDetailsByUserNameAsync(string userName);
    Task<Guid> GetUserIdAsync(string userName);
    Task<string?> GetUserNameAsync(Guid userId);
}