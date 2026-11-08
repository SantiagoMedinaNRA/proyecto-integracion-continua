namespace Api.Brokers;

public interface IPotterDbBroker
{
    ValueTask<List<BookDto>> GetBooksAsync();
    ValueTask<BookDto> GetBookByIdAsync(string bookId);
    ValueTask<List<CharacterDto>> GetCharactersAsync(int pageNumber = 1, int pageSize = 20);
    ValueTask<CharacterDto> GetCharacterByIdAsync(string characterId);
    ValueTask<List<SpellDto>> GetSpellsAsync();
    ValueTask<SpellDto> GetSpellByIdAsync(string spellId);
    ValueTask<List<PotionDto>> GetPotionsAsync();
    ValueTask<PotionDto> GetPotionByIdAsync(string potionId);
    ValueTask<List<MovieDto>> GetMoviesAsync();
    ValueTask<MovieDto> GetMovieByIdAsync(string movieId);
}
