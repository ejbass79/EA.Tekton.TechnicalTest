using EA.Tekton.TechnicalTest.WebApi.Requests.Base;

namespace EA.Tekton.TechnicalTest.WebApi.Requests;

public class UserRoleRequest : UserBase
{
    public string RoleName { get; set; } = string.Empty;
}
