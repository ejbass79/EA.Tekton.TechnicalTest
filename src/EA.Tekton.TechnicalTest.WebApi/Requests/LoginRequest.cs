using EA.Tekton.TechnicalTest.WebApi.Requests.Base;

namespace EA.Tekton.TechnicalTest.WebApi.Requests;

public class LoginRequest : UserBase
{
    public string Password { get; set; } = string.Empty;
}
