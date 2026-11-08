namespace Shared.Models;

/// <summary>
/// DTO for displaying movie information in the UI.
/// Simplified flat structure extracted from PotterDB's nested attributes.
/// </summary>
public class MovieDto
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? ReleaseDate { get; set; }
    public string? Directors { get; set; }
    public string? Producers { get; set; }
    public string? Screenwriters { get; set; }
    public string? RunningTime { get; set; }
    public string? Budget { get; set; }
    public string? BoxOffice { get; set; }
    public string? Cinematographers { get; set; }
    public string? Composers { get; set; }
    public string? Poster { get; set; }
    public string? Wiki { get; set; }
    public string? Summary { get; set; }

    /// <summary>
    /// Convert a MovieResponse from the API into a simplified MovieDto for display.
    /// </summary>
    public static MovieDto FromResponse(MovieResponse response)
    {
        return new MovieDto
        {
            Id = response.Id,
            Title = response.Attributes.Title,
            ReleaseDate = response.Attributes.ReleaseDate,
            Directors = FormatList(response.Attributes.Directors),
            Producers = FormatList(response.Attributes.Producers),
            Screenwriters = FormatList(response.Attributes.Screenwriters),
            RunningTime = response.Attributes.RunningTime,
            Budget = response.Attributes.Budget,
            BoxOffice = response.Attributes.BoxOffice,
            Cinematographers = FormatList(response.Attributes.Cinematographers),
            Composers = FormatList(response.Attributes.Composers),
            Poster = response.Attributes.Poster,
            Wiki = response.Attributes.Wiki,
            Summary = response.Attributes.Summary
        };
    }

    private static string? FormatList(List<string> list)
    {
        if (list == null || list.Count == 0)
            return null;
        return string.Join(", ", list);
    }
}
