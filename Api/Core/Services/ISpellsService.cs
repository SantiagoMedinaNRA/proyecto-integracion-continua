namespace Api.Core.Services;

public interface ISpellsService
{
    ValueTask<List<SpellDto>> GetSpellsAsync();
    ValueTask<SpellDto> GetSpellByIdAsync(string spellId);
}
