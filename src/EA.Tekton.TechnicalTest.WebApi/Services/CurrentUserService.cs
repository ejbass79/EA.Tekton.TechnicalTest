using EA.Tekton.TechnicalTest.Cross.Interfaces;

using System.Security.Claims;

namespace EA.Tekton.TechnicalTest.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId { get; }
        public string Email { get; }
        public bool IsAuthenticated { get; }
        public string IpAddress { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            IpAddress = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? string.Empty;
            IsAuthenticated = UserId != null;
        }
    }
}
