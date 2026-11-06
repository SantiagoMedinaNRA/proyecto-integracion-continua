namespace Api.Core.Services;

public interface ICharactersService
{
    ValueTask<List<CharacterDto>> GetCharactersAsync(int pageNumber = 1, int pageSize = 20);
    ValueTask<CharacterDto> GetCharacterByIdAsync(string characterId);
}
