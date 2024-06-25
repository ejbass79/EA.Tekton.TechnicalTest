namespace EA.Tekton.TechnicalTest.Cross.Auth.Models;

public class AuthResult
{
    public bool Succeeded { get; set; }
    public IEnumerable<string> Errors { get; set; }

    internal AuthResult(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors;
    }

    public static AuthResult Success()
    {
        return new AuthResult(true, Enumerable.Empty<string>());
    }

    public static AuthResult Failure(IEnumerable<string> errors)
    {
        return new AuthResult(false, errors);
    }
}
