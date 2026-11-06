namespace Api.Brokers;

public interface IPotterDbBroker
{
    ValueTask<List<BookResponse>> GetBooksAsync();
    ValueTask<BookResponse> GetBookByIdAsync(string bookId);
    ValueTask<List<CharacterResponse>> GetCharactersAsync();
    ValueTask<CharacterResponse> GetCharacterByIdAsync(string characterId);
}
