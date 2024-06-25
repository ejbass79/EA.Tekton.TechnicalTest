using EA.Tekton.TechnicalTest.WebApi.Requests.Base;

namespace EA.Tekton.TechnicalTest.WebApi.Requests;

public class RegisterRequest : UserBase
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
