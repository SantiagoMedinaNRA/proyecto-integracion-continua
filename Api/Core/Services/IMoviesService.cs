namespace Api.Core.Services;

public interface IMoviesService
{
    ValueTask<List<MovieDto>> GetMoviesAsync();
    ValueTask<MovieDto> GetMovieByIdAsync(string movieId);
}
