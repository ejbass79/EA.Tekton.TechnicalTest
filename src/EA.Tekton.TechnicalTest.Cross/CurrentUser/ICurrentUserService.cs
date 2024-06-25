namespace EA.Tekton.TechnicalTest.Cross.CurrentUser;

public interface ICurrentUserService
{
    public string UserId { get; }
    public string Email { get; }
    public bool IsAuthenticated { get; }
    public string IpAddress { get; }
}
