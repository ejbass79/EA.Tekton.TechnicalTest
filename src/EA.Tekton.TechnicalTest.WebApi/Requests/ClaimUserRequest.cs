using EA.Tekton.TechnicalTest.WebApi.Requests.Base;

namespace EA.Tekton.TechnicalTest.WebApi.Requests;

public class ClaimUserRequest : UserBase
{
    public string ClaimName { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}
