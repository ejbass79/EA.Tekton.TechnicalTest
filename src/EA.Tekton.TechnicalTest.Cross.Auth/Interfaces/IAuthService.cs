using EA.Tekton.TechnicalTest.Cross.Auth.Models;

namespace EA.Tekton.TechnicalTest.Cross.Auth.Interfaces;

public interface IAuthService
{
    Task<IList<string>?> GetUserRolesAsync(string email);

    Task<AuthUser> GetUserAsync(string email);

    Task<AuthResult> PasswordSignInAsync(string email, string password, bool lockoutOnFailure);

    Task<AuthResult> RegisterUserAsync(AuthUser authUser, string password);
}