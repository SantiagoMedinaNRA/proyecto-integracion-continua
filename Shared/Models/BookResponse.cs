namespace Shared.Models;

/// <summary>
/// Represents a book from the PotterDB API. Maps to the attributes section of a book resource in JSON:API format.
/// </summary>
public class BookResponse
{
  [JsonPropertyName("id")]
  public string Id { get; set; } = string.Empty;

  [JsonPropertyName("type")]
  public string Type { get; set; } = "book";

  [JsonPropertyName("attributes")]
  public BookAttributes Attributes { get; set; } = new();
}

/// <summary>
/// Represents the attributes of a book from PotterDB.
/// </summary>
public class BookAttributes
{
  [JsonPropertyName("slug")]
  public string Slug { get; set; } = string.Empty;

  [JsonPropertyName("author")]
  public string Author { get; set; } = string.Empty;

  [JsonPropertyName("cover")]
  public string Cover { get; set; } = string.Empty;

  [JsonPropertyName("dedication")]
  public string Dedication { get; set; } = string.Empty;

  [JsonPropertyName("pages")]
  public int Pages { get; set; }

  [JsonPropertyName("release_date")]
  public string ReleaseDate { get; set; } = string.Empty;

  [JsonPropertyName("summary")]
  public string Summary { get; set; } = string.Empty;

  [JsonPropertyName("title")]
  public string Title { get; set; } = string.Empty;

  [JsonPropertyName("wiki")]
  public string Wiki { get; set; } = string.Empty;
}