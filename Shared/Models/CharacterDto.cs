namespace Shared.Models;

/// <summary>
/// DTO for displaying character information in the UI.
/// Simplified flat structure extracted from PotterDB's nested attributes.
/// </summary>
public class CharacterDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? House { get; set; }
    public string? Patronus { get; set; }
    public string? BloodStatus { get; set; }
    public string? Species { get; set; }
    public string? Gender { get; set; }
    public string? Image { get; set; }
    public string? Wiki { get; set; }

    /// <summary>
    /// Get the wand information as a formatted string.
    /// PotterDB stores wands as a list of objects, we extract readable format.
    /// </summary>
    public string? GetWandInfo()
    {
        if (string.IsNullOrEmpty(Wand))
            return null;
        return Wand;
    }

    public string? Wand { get; set; }

    /// <summary>
    /// Convert a CharacterResponse from the API into a simplified CharacterDto for display.
    /// </summary>
    public static CharacterDto FromResponse(CharacterResponse response)
    {
        return new CharacterDto
        {
            Id = response.Id,
            Name = response.Attributes.Name,
            House = response.Attributes.House,
            Patronus = response.Attributes.Patronus,
            BloodStatus = response.Attributes.BloodStatus,
            Species = response.Attributes.Species,
            Gender = response.Attributes.Gender,
            Image = response.Attributes.Image,
            Wiki = response.Attributes.Wiki,
            Wand = FormatWands(response.Attributes.Wands)
        };
    }

    private static string? FormatWands(List<object> wands)
    {
        if (wands == null || wands.Count == 0)
            return null;

        // Wands are complex objects, just indicate if present
        return $"{wands.Count} wand(s)";
    }
}
