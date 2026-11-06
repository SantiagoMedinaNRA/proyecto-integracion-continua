namespace Api.Core.Services;

public interface IBooksService
{
    ValueTask<List<BookResponse>> GetBooksAsync();
    ValueTask<BookResponse> GetBookByIdAsync(string bookId);
}
