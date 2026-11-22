using Api.Brokers;
using Api.Core.Services;
using Microsoft.Extensions.Logging;
using Shared.Constants;
using Shared.Models;

namespace Api.UnitTests.Core.Services;

public class MoviesServiceTests
{
    private readonly IPotterDbBroker potterDbBroker;
    private readonly ICacheBroker cacheBroker;
    private readonly ILogger<MoviesService> logger;
    private readonly MoviesService moviesService;

    public MoviesServiceTests()
    {
        potterDbBroker = Substitute.For<IPotterDbBroker>();
        cacheBroker = Substitute.For<ICacheBroker>();
        logger = Substitute.For<ILogger<MoviesService>>();
        moviesService = new MoviesService(potterDbBroker, cacheBroker, logger);
    }

    [Fact]
    public async Task GetMoviesAsync_WhenCacheMiss_FetchesFromBrokerAndCaches()
    {
        // Given
        var expectedMovies = new List<MovieDto>
        {
            new() { Id = "movie-1", Title = "Harry Potter and the Philosopher's Stone", Directors = "Chris Columbus" },
            new() { Id = "movie-2", Title = "Harry Potter and the Chamber of Secrets", Directors = "Chris Columbus" }
        };

        cacheBroker.GetAsync<List<MovieDto>>(Arg.Any<string>()).Returns((List<MovieDto>?)null);
        potterDbBroker.GetMoviesAsync().Returns(expectedMovies);

        // When
        var result = await moviesService.GetMoviesAsync();

        // Then
        result.ShouldBe(expectedMovies);
        result.Count.ShouldBe(2);
        await potterDbBroker.Received(1).GetMoviesAsync();
        await cacheBroker.Received(1).SetAsync(CacheKeys.AllMovies, expectedMovies);
        logger.ReceivedWithAnyArgs().LogInformation("Fetching movies from PotterDB");
    }

    [Fact]
    public async Task GetMoviesAsync_WhenCacheHit_ReturnsCachedMovies()
    {
        // Given
        var cachedMovies = new List<MovieDto>
        {
            new() { Id = "movie-1", Title = "Harry Potter and the Philosopher's Stone" }
        };

        cacheBroker.GetAsync<List<MovieDto>>(CacheKeys.AllMovies).Returns(cachedMovies);

        // When
        var result = await moviesService.GetMoviesAsync();

        // Then
        result.ShouldBe(cachedMovies);
        result.Count.ShouldBe(1);
        await potterDbBroker.DidNotReceive().GetMoviesAsync();
        logger.ReceivedWithAnyArgs().LogInformation("Returning cached movies");
    }

    [Fact]
    public async Task GetMovieByIdAsync_WhenCacheMiss_FetchesFromBrokerAndCaches()
    {
        // Given
        var movieId = "movie-1";
        var expectedMovie = new MovieDto
        {
            Id = movieId,
            Title = "Harry Potter and the Philosopher's Stone",
            Directors = "Chris Columbus",
            ReleaseDate = "2001-11-04"
        };

        cacheBroker.GetAsync<MovieDto>(Arg.Any<string>()).Returns((MovieDto?)null);
        potterDbBroker.GetMovieByIdAsync(movieId).Returns(expectedMovie);

        // When
        var result = await moviesService.GetMovieByIdAsync(movieId);

        // Then
        result.ShouldBe(expectedMovie);
        result.Id.ShouldBe(movieId);
        result.Title.ShouldBe("Harry Potter and the Philosopher's Stone");
        await potterDbBroker.Received(1).GetMovieByIdAsync(movieId);
        logger.ReceivedWithAnyArgs().LogInformation("Fetching movie {MovieId} from PotterDB", movieId);
    }

    [Fact]
    public async Task GetMovieByIdAsync_WhenCacheHit_ReturnsCachedMovie()
    {
        // Given
        var movieId = "movie-1";
        var cachedMovie = new MovieDto { Id = movieId, Title = "Harry Potter and the Philosopher's Stone" };
        var cacheKey = string.Format(CacheKeys.MovieDetail, movieId);

        cacheBroker.GetAsync<MovieDto>(cacheKey).Returns(cachedMovie);

        // When
        var result = await moviesService.GetMovieByIdAsync(movieId);

        // Then
        result.ShouldBe(cachedMovie);
        await potterDbBroker.DidNotReceive().GetMovieByIdAsync(Arg.Any<string>());
        logger.ReceivedWithAnyArgs().LogInformation("Returning cached movie {MovieId}", movieId);
    }

    [Fact]
    public async Task GetMoviesAsync_WhenBrokerThrows_LogsErrorAndRethrows()
    {
        // Given
        cacheBroker.GetAsync<List<MovieDto>>(Arg.Any<string>()).Returns((List<MovieDto>?)null);
        potterDbBroker.GetMoviesAsync().Returns<ValueTask<List<MovieDto>>>(x => { throw new Exception("PotterDB unavailable"); });

        // When & Then
        var act = async () => await moviesService.GetMoviesAsync();
        await act.ShouldThrowAsync<Exception>();
        logger.ReceivedWithAnyArgs().LogError(Arg.Any<Exception>(), "Error retrieving movies");
    }
}