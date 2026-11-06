namespace Shared.Models;

/// <summary>
/// DTO for displaying book information in the UI. Simplified flat structure extracted from PotterDB's nested attributes.
/// </summary>
public class BookDto
{
  public string Id { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public string Author { get; set; } = string.Empty;
  public int Pages { get; set; }
  public DateTime ReleaseDate { get; set; }
  public string Description { get; set; } = string.Empty;
  public string? Cover { get; set; }
  public string? Wiki { get; set; }

  /// <summary>
  /// Convert a BookResponse from the API into a simplified BookDto for display.
  /// </summary>
  public static BookDto FromResponse(BookResponse response)
  {
    return new BookDto
    {
      Id = response.Id,
      Title = response.Attributes.Title,
      Author = response.Attributes.Author,
      Pages = response.Attributes.Pages,
      ReleaseDate = DateTime.TryParse(response.Attributes.ReleaseDate, out var date)
            ? date
            : DateTime.MinValue,
      Description = response.Attributes.Summary,
      Cover = response.Attributes.Cover,
      Wiki = response.Attributes.Wiki
    };
  }
}