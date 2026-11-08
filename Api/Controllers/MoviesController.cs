namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMoviesService moviesService;
    private readonly ILogger<MoviesController> logger;

    public MoviesController(IMoviesService moviesService, ILogger<MoviesController> logger)
    {
        this.moviesService = moviesService;
        this.logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
    {
        try
        {
            logger.LogInformation("GET /api/movies");
            List<MovieDto> movies = await moviesService.GetMoviesAsync();
            return Ok(movies);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error in GET /api/movies");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving movies" });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MovieDto>> GetMovieById(string id)
    {
        try
        {
            logger.LogInformation("GET /api/movies/{Id}", id);
            MovieDto movie = await moviesService.GetMovieByIdAsync(id);
            return Ok(movie);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error in GET /api/movies/{Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving movie" });
        }
    }
}
