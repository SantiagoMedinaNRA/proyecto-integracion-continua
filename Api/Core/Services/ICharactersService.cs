namespace Api.Core.Services;

public interface ICharactersService
{
    ValueTask<List<CharacterResponse>> GetCharactersAsync();
    ValueTask<CharacterResponse> GetCharacterByIdAsync(string characterId);
}
