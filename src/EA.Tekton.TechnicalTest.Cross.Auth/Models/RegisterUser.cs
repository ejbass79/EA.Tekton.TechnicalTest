namespace EA.Tekton.TechnicalTest.Cross.Auth.Models;

internal class RegisterUser : AuthUser
{
    public string PhoneNumber { get; set; } = string.Empty;
}
