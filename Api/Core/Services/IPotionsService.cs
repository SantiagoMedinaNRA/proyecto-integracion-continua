namespace Api.Core.Services;

public interface IPotionsService
{
    ValueTask<List<PotionDto>> GetPotionsAsync();
    ValueTask<PotionDto> GetPotionByIdAsync(string potionId);
}
