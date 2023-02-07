using InnoGotchi.Domain.Common;

namespace InnoGotchi.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> SignIn(string userId, string password, bool isPersistent);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task SignOut();

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);
}
