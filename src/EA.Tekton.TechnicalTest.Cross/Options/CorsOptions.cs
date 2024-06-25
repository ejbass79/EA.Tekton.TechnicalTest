namespace EA.Tekton.TechnicalTest.Cross.Options;

public class CorsOptions
{
    public string Policy { get; set; } = string.Empty;
    public List<string> Origins { get; set; } = new();
}
