namespace Api.Brokers;

public interface IPotterDbBroker
{
    ValueTask<List<BookDto>> GetBooksAsync();
    ValueTask<BookDto> GetBookByIdAsync(string bookId);
    ValueTask<List<CharacterDto>> GetCharactersAsync(int pageNumber = 1, int pageSize = 20);
    ValueTask<CharacterDto> GetCharacterByIdAsync(string characterId);
}
