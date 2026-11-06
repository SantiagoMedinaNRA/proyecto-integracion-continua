namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBooksService booksService;
    private readonly ILogger<BooksController> logger;

    public BooksController(IBooksService booksService, ILogger<BooksController> logger)
    {
        this.booksService = booksService;
        this.logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<BookResponse>>> GetBooks()
    {
        try
        {
            logger.LogInformation("GET /api/books");
            List<BookResponse> books = await booksService.GetBooksAsync();
            return Ok(books);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error in GET /api/books");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving books" });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BookResponse>> GetBookById(string id)
    {
        try
        {
            logger.LogInformation("GET /api/books/{Id}", id);
            BookResponse book = await booksService.GetBookByIdAsync(id);
            return Ok(book);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error in GET /api/books/{Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving book" });
        }
    }
}
