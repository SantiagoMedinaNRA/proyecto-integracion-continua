namespace Shared.Models;

/// <summary>
/// Represents a character from the PotterDB API.
/// Maps to the attributes section of a character resource in JSON:API format.
/// </summary>
public class CharacterResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = "character";

    [JsonPropertyName("attributes")]
    public CharacterAttributes Attributes { get; set; } = new();
}

/// <summary>
/// Represents the attributes of a character from PotterDB.
/// </summary>
public class CharacterAttributes
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("alias_names")]
    public List<string> AliasNames { get; set; } = [];

    [JsonPropertyName("animagus")]
    public string? Animagus { get; set; }

    [JsonPropertyName("blood_status")]
    public string? BloodStatus { get; set; }

    [JsonPropertyName("boggart")]
    public string? Boggart { get; set; }

    [JsonPropertyName("born")]
    public string? Born { get; set; }

    [JsonPropertyName("died")]
    public string? Died { get; set; }

    [JsonPropertyName("eye_color")]
    public string? EyeColor { get; set; }

    [JsonPropertyName("family_members")]
    public List<string> FamilyMembers { get; set; } = [];

    [JsonPropertyName("gender")]
    public string? Gender { get; set; }

    [JsonPropertyName("hair_color")]
    public string? HairColor { get; set; }

    [JsonPropertyName("height")]
    public string? Height { get; set; }

    [JsonPropertyName("house")]
    public string? House { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }

    [JsonPropertyName("jobs")]
    public List<string> Jobs { get; set; } = [];

    [JsonPropertyName("marital_status")]
    public string? MaritalStatus { get; set; }

    [JsonPropertyName("nationality")]
    public string? Nationality { get; set; }

    [JsonPropertyName("patronus")]
    public string? Patronus { get; set; }

    [JsonPropertyName("romances")]
    public List<string> Romances { get; set; } = [];

    [JsonPropertyName("skin_color")]
    public string? SkinColor { get; set; }

    [JsonPropertyName("species")]
    public string? Species { get; set; }

    [JsonPropertyName("titles")]
    public List<string> Titles { get; set; } = [];

    [JsonPropertyName("wands")]
    public List<object> Wands { get; set; } = [];

    [JsonPropertyName("weight")]
    public string? Weight { get; set; }

    [JsonPropertyName("wiki")]
    public string? Wiki { get; set; }
}
