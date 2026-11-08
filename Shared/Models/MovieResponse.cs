namespace Shared.Models;

/// <summary>
/// Represents a movie from the PotterDB API.
/// Maps to the attributes section of a movie resource in JSON:API format.
/// </summary>
public class MovieResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = "movie";

    [JsonPropertyName("attributes")]
    public MovieAttributes Attributes { get; set; } = new();
}

/// <summary>
/// Represents the attributes of a movie from PotterDB.
/// </summary>
public class MovieAttributes
{
    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("release_date")]
    public string? ReleaseDate { get; set; }

    [JsonPropertyName("directors")]
    public List<string> Directors { get; set; } = [];

    [JsonPropertyName("producers")]
    public List<string> Producers { get; set; } = [];

    [JsonPropertyName("screenwriters")]
    public List<string> Screenwriters { get; set; } = [];

    [JsonPropertyName("running_time")]
    public string? RunningTime { get; set; }

    [JsonPropertyName("budget")]
    public string? Budget { get; set; }

    [JsonPropertyName("box_office")]
    public string? BoxOffice { get; set; }

    [JsonPropertyName("cinematographers")]
    public List<string> Cinematographers { get; set; } = [];

    [JsonPropertyName("music_composers")]
    public List<string> Composers { get; set; } = [];

    [JsonPropertyName("poster")]
    public string? Poster { get; set; }

    [JsonPropertyName("wiki")]
    public string? Wiki { get; set; }

    [JsonPropertyName("summary")]
    public string? Summary { get; set; }
}
