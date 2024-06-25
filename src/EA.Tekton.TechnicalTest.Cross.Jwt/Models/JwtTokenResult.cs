namespace EA.Tekton.TechnicalTest.Cross.Jwt.Models;

public class JwtTokenResult
{
    public bool Succeeded { get; set; }
    public string Error { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
}
