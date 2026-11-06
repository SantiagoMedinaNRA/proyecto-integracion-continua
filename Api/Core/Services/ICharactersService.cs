namespace Api.Core.Services;

public interface ICharactersService
{
    ValueTask<List<CharacterDto>> GetCharactersAsync();
    ValueTask<CharacterDto> GetCharacterByIdAsync(string characterId);
}
