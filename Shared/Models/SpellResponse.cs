namespace Shared.Models;

/// <summary>
/// Represents a spell from the PotterDB API.
/// Maps to the attributes section of a spell resource in JSON:API format.
/// </summary>
public class SpellResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = "spell";

    [JsonPropertyName("attributes")]
    public SpellAttributes Attributes { get; set; } = new();
}

/// <summary>
/// Represents the attributes of a spell from PotterDB.
/// </summary>
public class SpellAttributes
{
    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("incantation")]
    public string? Incantation { get; set; }

    [JsonPropertyName("category")]
    public string? Category { get; set; }

    [JsonPropertyName("effect")]
    public string? Effect { get; set; }

    [JsonPropertyName("hand_movements")]
    public string? HandMovements { get; set; }

    [JsonPropertyName("light")]
    public string? Light { get; set; }

    [JsonPropertyName("creator")]
    public string? Creator { get; set; }

    [JsonPropertyName("wiki")]
    public string? Wiki { get; set; }
}
