namespace Api.Brokers;

public interface IPotterDbBroker
{
    ValueTask<List<BookResponse>> GetBooksAsync();
    ValueTask<BookResponse> GetBookByIdAsync(string bookId);
}
