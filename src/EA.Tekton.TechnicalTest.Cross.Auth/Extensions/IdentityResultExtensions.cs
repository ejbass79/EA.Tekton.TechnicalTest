using EA.Tekton.TechnicalTest.Cross.Auth.Models;

using Microsoft.AspNetCore.Identity;

namespace EA.Tekton.TechnicalTest.Cross.Auth.Extensions;

public static class IdentityResultExtensions
{
    public static AuthResult MapToResult(this IdentityResult result)
    {
        return result.Succeeded
            ? AuthResult.Success()
            : AuthResult.Failure(result.Errors.Select(e => e.Description));
    }

    public static AuthResult MapToResult(this SignInResult result)
    {
        return result.Succeeded
            ? AuthResult.Success()
            : AuthResult.Failure(new string[] { "Invalid login attempt." });
    }
}
