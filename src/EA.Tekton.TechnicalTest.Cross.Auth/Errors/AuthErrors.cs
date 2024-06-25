using EA.Tekton.TechnicalTest.Cross.Abstractions;

namespace EA.Tekton.TechnicalTest.Cross.Auth.Errors;

public static class AuthErrors
{
    public static Error NotFound = new("Auth.NotFound", "Token was not found.");
    public static Error FailedRegister = new("Auth.FailedRegister", "Failed to register user.");
    public static Error FailedNotifyReset = new("Users.FailedNotifyReset", "Could not reset user Password.");
}
