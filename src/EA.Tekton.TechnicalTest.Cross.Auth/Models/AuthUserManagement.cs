namespace EA.Tekton.TechnicalTest.Cross.Auth.Models;

public class AuthUserManagement
{
    public string FullName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; } = false;
    public bool LockoutEnabled { get; set; } = false;
    public IList<string> Roles { get; set; } = [];
}
