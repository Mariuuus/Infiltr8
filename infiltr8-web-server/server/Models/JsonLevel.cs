using System.Text.Json.Serialization;

public class JsonLevel
{

    // public string Id { get; set; }
    [JsonRequired]
    [JsonPropertyName("content")]
    public string Content { get; set; }
}