using System.Text.Json.Serialization;

public class JsonLevel
{
    [JsonRequired]
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonRequired]
    [JsonPropertyName("author")]
    public required string Author { get; set; }

    [JsonRequired]
    [JsonPropertyName("content")]
    public required string Content { get; set; }
}

public class LevelSummary
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Author { get; set; }
}
