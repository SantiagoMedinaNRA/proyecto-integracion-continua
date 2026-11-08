namespace Shared.Models;

/// <summary>
/// DTO for displaying potion information in the UI.
/// Simplified flat structure extracted from PotterDB's nested attributes.
/// </summary>
public class PotionDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Effect { get; set; }
    public string? Difficulty { get; set; }
    public string? Characteristics { get; set; }
    public string? Inventors { get; set; }
    public string? Ingredients { get; set; }
    public string? Manufacturers { get; set; }
    public string? SideEffects { get; set; }
    public string? Time { get; set; }
    public string? Image { get; set; }
    public string? Wiki { get; set; }

    /// <summary>
    /// Convert a PotionResponse from the API into a simplified PotionDto for display.
    /// </summary>
    public static PotionDto FromResponse(PotionResponse response)
    {
        return new PotionDto
        {
            Id = response.Id,
            Name = response.Attributes.Name,
            Effect = response.Attributes.Effect,
            Difficulty = response.Attributes.Difficulty,
            Characteristics = response.Attributes.Characteristics,
            Inventors = response.Attributes.Inventors,
            Ingredients = response.Attributes.Ingredients,
            Manufacturers = response.Attributes.Manufacturers,
            SideEffects = response.Attributes.SideEffects,
            Time = response.Attributes.Time,
            Image = response.Attributes.Image,
            Wiki = response.Attributes.Wiki
        };
    }
}
