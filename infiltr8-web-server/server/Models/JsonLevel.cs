using System.Text.Json.Serialization;

public class JsonLevel
{
    [JsonRequired]
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonRequired]
    [JsonPropertyName("content")]
    public required string Content { get; set; }

    [JsonRequired]
    [JsonPropertyName("username")]
    public string Username { get; set; }
}

public class LevelSummary
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Author { get; set; }
}
