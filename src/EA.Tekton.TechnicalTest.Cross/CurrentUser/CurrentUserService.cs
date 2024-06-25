using Microsoft.AspNetCore.Http;

using System.Security.Claims;

namespace EA.Tekton.TechnicalTest.Cross.CurrentUser;

public class CurrentUserService : ICurrentUserService
{
    public string UserId { get; }
    public string Email { get; }
    public bool IsAuthenticated { get; }
    public string IpAddress { get; }

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;
     
        UserId = user?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        Email = user?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

        var connection = httpContextAccessor.HttpContext?.Connection;
        
        IpAddress = connection?.RemoteIpAddress?.ToString() ?? string.Empty;

        IsAuthenticated = !string.IsNullOrEmpty(UserId);
    }
}
