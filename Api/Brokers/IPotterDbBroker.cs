namespace Api.Brokers;

public interface IPotterDbBroker
{
    ValueTask<List<BookDto>> GetBooksAsync();
    ValueTask<BookDto> GetBookByIdAsync(string bookId);
    ValueTask<List<CharacterDto>> GetCharactersAsync();
    ValueTask<CharacterDto> GetCharacterByIdAsync(string characterId);
}
