namespace Shared.Models;

/// <summary>
/// DTO for displaying spell information in the UI.
/// Simplified flat structure extracted from PotterDB's nested attributes.
/// </summary>
public class SpellDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Incantation { get; set; }
    public string? Category { get; set; }
    public string? Effect { get; set; }
    public string? HandMovements { get; set; }
    public string? Light { get; set; }
    public string? Creator { get; set; }
    public string? Wiki { get; set; }

    /// <summary>
    /// Convert a SpellResponse from the API into a simplified SpellDto for display.
    /// </summary>
    public static SpellDto FromResponse(SpellResponse response)
    {
        return new SpellDto
        {
            Id = response.Id,
            Name = response.Attributes.Name,
            Incantation = response.Attributes.Incantation,
            Category = response.Attributes.Category,
            Effect = response.Attributes.Effect,
            HandMovements = response.Attributes.HandMovements,
            Light = response.Attributes.Light,
            Creator = response.Attributes.Creator,
            Wiki = response.Attributes.Wiki
        };
    }
}
