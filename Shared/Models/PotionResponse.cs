namespace Shared.Models;

/// <summary>
/// Represents a potion from the PotterDB API.
/// Maps to the attributes section of a potion resource in JSON:API format.
/// </summary>
public class PotionResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = "potion";

    [JsonPropertyName("attributes")]
    public PotionAttributes Attributes { get; set; } = new();
}

/// <summary>
/// Represents the attributes of a potion from PotterDB.
/// </summary>
public class PotionAttributes
{
    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("effect")]
    public string? Effect { get; set; }

    [JsonPropertyName("difficulty")]
    public string? Difficulty { get; set; }

    [JsonPropertyName("characteristics")]
    public string? Characteristics { get; set; }

    [JsonPropertyName("inventors")]
    public string? Inventors { get; set; }

    [JsonPropertyName("ingredients")]
    public string? Ingredients { get; set; }

    [JsonPropertyName("manufacturers")]
    public string? Manufacturers { get; set; }

    [JsonPropertyName("side_effects")]
    public string? SideEffects { get; set; }

    [JsonPropertyName("time")]
    public string? Time { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }

    [JsonPropertyName("wiki")]
    public string? Wiki { get; set; }
}
