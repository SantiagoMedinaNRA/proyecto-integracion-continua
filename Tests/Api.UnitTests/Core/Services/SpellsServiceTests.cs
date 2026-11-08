using Api.Brokers;
using Api.Core.Services;
using Microsoft.Extensions.Logging;
using Shared.Constants;
using Shared.Models;

namespace Api.UnitTests.Core.Services;

public class SpellsServiceTests
{
    private readonly IPotterDbBroker potterDbBroker;
    private readonly ICacheBroker cacheBroker;
    private readonly ILogger<SpellsService> logger;
    private readonly SpellsService spellsService;

    public SpellsServiceTests()
    {
        potterDbBroker = Substitute.For<IPotterDbBroker>();
        cacheBroker = Substitute.For<ICacheBroker>();
        logger = Substitute.For<ILogger<SpellsService>>();
        spellsService = new SpellsService(potterDbBroker, cacheBroker, logger);
    }

    [Fact]
    public async Task GetSpellsAsync_WhenCacheMiss_FetchesFromBrokerAndCaches()
    {
        // Given
        var expectedSpells = new List<SpellDto>
        {
            new() { Id = "spell-1", Name = "Expelliarmus", Incantation = "Expelliarmus", Effect = "Disarms the opponent" },
            new() { Id = "spell-2", Name = "Lumos", Incantation = "Lumos", Effect = "Creates light" }
        };

        cacheBroker.GetAsync<List<SpellDto>>(Arg.Any<string>()).Returns((List<SpellDto>?)null);
        potterDbBroker.GetSpellsAsync().Returns(expectedSpells);

        // When
        var result = await spellsService.GetSpellsAsync();

        // Then
        result.ShouldBe(expectedSpells);
        result.Count.ShouldBe(2);
        await potterDbBroker.Received(1).GetSpellsAsync();
        await cacheBroker.Received(1).SetAsync(CacheKeys.AllSpells, expectedSpells);
        logger.ReceivedWithAnyArgs().LogInformation("Fetching spells from PotterDB");
    }

    [Fact]
    public async Task GetSpellsAsync_WhenCacheHit_ReturnsCachedSpells()
    {
        // Given
        var cachedSpells = new List<SpellDto>
        {
            new() { Id = "spell-1", Name = "Expelliarmus" }
        };

        cacheBroker.GetAsync<List<SpellDto>>(CacheKeys.AllSpells).Returns(cachedSpells);

        // When
        var result = await spellsService.GetSpellsAsync();

        // Then
        result.ShouldBe(cachedSpells);
        result.Count.ShouldBe(1);
        await potterDbBroker.DidNotReceive().GetSpellsAsync();
        logger.ReceivedWithAnyArgs().LogInformation("Returning cached spells");
    }

    [Fact]
    public async Task GetSpellByIdAsync_WhenCacheMiss_FetchesFromBrokerAndCaches()
    {
        // Given
        var spellId = "spell-1";
        var expectedSpell = new SpellDto
        {
            Id = spellId,
            Name = "Expelliarmus",
            Incantation = "Expelliarmus",
            Effect = "Disarms the opponent"
        };

        cacheBroker.GetAsync<SpellDto>(Arg.Any<string>()).Returns((SpellDto?)null);
        potterDbBroker.GetSpellByIdAsync(spellId).Returns(expectedSpell);

        // When
        var result = await spellsService.GetSpellByIdAsync(spellId);

        // Then
        result.ShouldBe(expectedSpell);
        result.Id.ShouldBe(spellId);
        result.Name.ShouldBe("Expelliarmus");
        await potterDbBroker.Received(1).GetSpellByIdAsync(spellId);
        logger.ReceivedWithAnyArgs().LogInformation("Fetching spell {SpellId} from PotterDB", spellId);
    }

    [Fact]
    public async Task GetSpellByIdAsync_WhenCacheHit_ReturnsCachedSpell()
    {
        // Given
        var spellId = "spell-1";
        var cachedSpell = new SpellDto { Id = spellId, Name = "Expelliarmus" };
        var cacheKey = string.Format(CacheKeys.SpellDetail, spellId);

        cacheBroker.GetAsync<SpellDto>(cacheKey).Returns(cachedSpell);

        // When
        var result = await spellsService.GetSpellByIdAsync(spellId);

        // Then
        result.ShouldBe(cachedSpell);
        await potterDbBroker.DidNotReceive().GetSpellByIdAsync(Arg.Any<string>());
        logger.ReceivedWithAnyArgs().LogInformation("Returning cached spell {SpellId}", spellId);
    }

    [Fact]
    public async Task GetSpellsAsync_WhenBrokerThrows_LogsErrorAndRethrows()
    {
        // Given
        cacheBroker.GetAsync<List<SpellDto>>(Arg.Any<string>()).Returns((List<SpellDto>?)null);
        potterDbBroker.GetSpellsAsync().Returns<ValueTask<List<SpellDto>>>(x => { throw new Exception("PotterDB unavailable"); });

        // When & Then
        var act = async () => await spellsService.GetSpellsAsync();
        await act.ShouldThrowAsync<Exception>();
        logger.ReceivedWithAnyArgs().LogError(Arg.Any<Exception>(), "Error retrieving spells");
    }
}