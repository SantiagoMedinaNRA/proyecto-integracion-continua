namespace Api.Core.Services;

public interface IBooksService
{
    ValueTask<List<BookDto>> GetBooksAsync();
    ValueTask<BookDto> GetBookByIdAsync(string bookId);
}
