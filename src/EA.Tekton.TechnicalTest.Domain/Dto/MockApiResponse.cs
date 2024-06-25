using Newtonsoft.Json;

namespace EA.Tekton.TechnicalTest.Domain.Dto;

public class MockApiResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("percent")]
    public int Percent { get; set; }
}