namespace Shared.Models;

/// <summary>
/// Represents the JSON:API response wrapper from PotterDB.
/// The actual data is in the "data" property.
/// </summary>
public class PotterDbResponse<T>
{
    [JsonPropertyName("data")]
    public List<T> Data { get; set; } = [];

    [JsonPropertyName("meta")]
    public PotterDbMeta? Meta { get; set; }

    [JsonPropertyName("links")]
    public Dictionary<string, string>? Links { get; set; }
}

/// <summary>
/// Represents metadata from PotterDB API responses (pagination, copyright, etc).
/// </summary>
public class PotterDbMeta
{
    [JsonPropertyName("pagination")]
    public PotterDbPagination? Pagination { get; set; }

    [JsonPropertyName("copyright")]
    public string? Copyright { get; set; }

    [JsonPropertyName("generated_at")]
    public string? GeneratedAt { get; set; }
}

/// <summary>
/// Represents pagination information from PotterDB API responses.
/// </summary>
public class PotterDbPagination
{
    [JsonPropertyName("current")]
    public int Current { get; set; }

    [JsonPropertyName("next")]
    public int? Next { get; set; }

    [JsonPropertyName("last")]
    public int Last { get; set; }

    [JsonPropertyName("records")]
    public int Records { get; set; }
}
