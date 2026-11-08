namespace Api.Core.Services;

public class MoviesService : IMoviesService
{
    private readonly IPotterDbBroker potterDbBroker;
    private readonly ICacheBroker cacheBroker;
    private readonly ILogger<MoviesService> logger;

    public MoviesService(IPotterDbBroker potterDbBroker, ICacheBroker cacheBroker, ILogger<MoviesService> logger)
    {
        this.potterDbBroker = potterDbBroker;
        this.cacheBroker = cacheBroker;
        this.logger = logger;
    }

    public async ValueTask<List<MovieDto>> GetMoviesAsync()
    {
        try
        {
            var cachedMovies = await cacheBroker.GetAsync<List<MovieDto>>(CacheKeys.AllMovies);

            if (cachedMovies != null)
            {
                logger.LogInformation("Returning cached movies");
                return cachedMovies;
            }

            logger.LogInformation("Fetching movies from PotterDB");
            List<MovieDto> movies = await potterDbBroker.GetMoviesAsync();

            await cacheBroker.SetAsync(CacheKeys.AllMovies, movies);

            return movies;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error retrieving movies");
            throw;
        }
    }

    public async ValueTask<MovieDto> GetMovieByIdAsync(string movieId)
    {
        try
        {
            string cacheKey = string.Format(CacheKeys.MovieDetail, movieId);
            var cachedMovie = await cacheBroker.GetAsync<MovieDto>(cacheKey);

            if (cachedMovie != null)
            {
                logger.LogInformation("Returning cached movie {MovieId}", movieId);
                return cachedMovie;
            }

            logger.LogInformation("Fetching movie {MovieId} from PotterDB", movieId);
            MovieDto movie = await potterDbBroker.GetMovieByIdAsync(movieId);

            await cacheBroker.SetAsync(cacheKey, movie);

            return movie;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error retrieving movie {MovieId}", movieId);
            throw;
        }
    }
}
