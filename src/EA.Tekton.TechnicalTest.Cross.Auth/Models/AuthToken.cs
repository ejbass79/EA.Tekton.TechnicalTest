using EA.Tekton.TechnicalTest.Cross.Dto;

namespace EA.Tekton.TechnicalTest.Cross.Auth.Models;

public class AuthToken
{
    public string AccessToken { get; set; } = string.Empty;
    public string TokenType { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public TokenInfo User { get; set; } = new();
    public List<string> Roles { get; set; } = [];
}
