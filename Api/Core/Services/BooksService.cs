namespace Api.Core.Services;

public class BooksService : IBooksService
{
    private readonly IPotterDbBroker potterDbBroker;
    private readonly ICacheBroker cacheBroker;
    private readonly ILogger<BooksService> logger;

    public BooksService(IPotterDbBroker potterDbBroker, ICacheBroker cacheBroker, ILogger<BooksService> logger)
    {
        this.potterDbBroker = potterDbBroker;
        this.cacheBroker = cacheBroker;
        this.logger = logger;
    }

    public async ValueTask<List<BookResponse>> GetBooksAsync()
    {
        try
        {
            var cachedBooks = await cacheBroker.GetAsync<List<BookResponse>>(CacheKeys.AllBooks);

            if (cachedBooks != null)
            {
                logger.LogInformation("Returning cached books");
                return cachedBooks;
            }

            logger.LogInformation("Fetching books from PotterDB");
            List<BookResponse> books = await potterDbBroker.GetBooksAsync();

            await cacheBroker.SetAsync(CacheKeys.AllBooks, books);

            return books;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error retrieving books");
            throw;
        }
    }

    public async ValueTask<BookResponse> GetBookByIdAsync(string bookId)
    {
        try
        {
            string cacheKey = string.Format(CacheKeys.BookDetail, bookId);
            var cachedBook = await cacheBroker.GetAsync<BookResponse>(cacheKey);

            if (cachedBook != null)
            {
                logger.LogInformation("Returning cached book {BookId}", bookId);
                return cachedBook;
            }

            logger.LogInformation("Fetching book {BookId} from PotterDB", bookId);
            BookResponse book = await potterDbBroker.GetBookByIdAsync(bookId);

            await cacheBroker.SetAsync(cacheKey, book);

            return book;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error retrieving book {BookId}", bookId);
            throw;
        }
    }
}
