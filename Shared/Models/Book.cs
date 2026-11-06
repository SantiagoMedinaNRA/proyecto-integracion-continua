namespace Shared.Models;

public record BookResponse(string Id, string Title, string Author, DateTime ReleaseDate, string Description, int Pages);
