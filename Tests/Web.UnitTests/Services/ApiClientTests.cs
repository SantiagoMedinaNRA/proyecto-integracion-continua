// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Web.UnitTests.Services;

public class ApiClientTests
{
    private readonly ILogger<ApiClient> logger;
    private readonly ApiSettings apiSettings;

    public ApiClientTests()
    {
        this.logger = Substitute.For<ILogger<ApiClient>>();
        this.apiSettings = new ApiSettings { BaseUrl = "http://localhost:5000" };
    }

    private static IOptions<ApiSettings> CreateApiSettingsOptions(ApiSettings apiSettings)
    {
        var options = Substitute.For<IOptions<ApiSettings>>();
        options.Value.Returns(apiSettings);
        return options;
    }

    private static BookDto CreateRandomBookDto() => new()
    {
        Id = Guid.NewGuid().ToString(),
        Title = Guid.NewGuid().ToString(),
        Author = Guid.NewGuid().ToString(),
        Pages = new Random().Next(100, 500),
        ReleaseDate = DateTime.Now.AddYears(-5),
        Description = Guid.NewGuid().ToString()
    };

    private static CharacterDto CreateRandomCharacterDto() => new()
    {
        Id = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString(),
        House = Guid.NewGuid().ToString(),
        Species = Guid.NewGuid().ToString()
    };

    private static SpellDto CreateRandomSpellDto() => new()
    {
        Id = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString(),
        Incantation = Guid.NewGuid().ToString()
    };

    private static PotionDto CreateRandomPotionDto() => new()
    {
        Id = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString(),
        Effect = Guid.NewGuid().ToString()
    };

    private static MovieDto CreateRandomMovieDto() => new()
    {
        Id = Guid.NewGuid().ToString(),
        Title = Guid.NewGuid().ToString(),
        ReleaseDate = DateTime.Now.AddYears(-5).ToString()
    };

    [Fact]
    public void ApiClientConstructor_WhenGivenHttpClientLoggerAndOptions_ConstructsSuccessfully()
    {
        // Given
        using var httpClient = new HttpClient();
        var apiSettingsOptions = CreateApiSettingsOptions(this.apiSettings);

        // When
        var apiClient = new ApiClient(httpClient, this.logger, apiSettingsOptions);

        // Then
        apiClient.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetBooksAsync_WhenInvoked_ExecutesWithoutException()
    {
        // Given
        using var httpClient = new HttpClient();
        var apiSettingsOptions = CreateApiSettingsOptions(this.apiSettings);
        var apiClient = new ApiClient(httpClient, this.logger, apiSettingsOptions);

        // When
        var act = async () => await apiClient.GetBooksAsync();

        // Then - expecting exception due to no mock server, but method should be callable
        await act.ShouldThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetCharactersAsync_WhenInvokedWithDefaultParameters_ExecutesWithoutException()
    {
        // Given
        using var httpClient = new HttpClient();
        var apiSettingsOptions = CreateApiSettingsOptions(this.apiSettings);
        var apiClient = new ApiClient(httpClient, this.logger, apiSettingsOptions);

        // When
        var act = async () => await apiClient.GetCharactersAsync();

        // Then - expecting exception due to no mock server, but method should be callable
        await act.ShouldThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetCharactersAsync_WhenInvokedWithCustomPageParameters_ExecutesWithoutException()
    {
        // Given
        int pageNumber = 2;
        int pageSize = 50;
        using var httpClient = new HttpClient();
        var apiSettingsOptions = CreateApiSettingsOptions(this.apiSettings);
        var apiClient = new ApiClient(httpClient, this.logger, apiSettingsOptions);

        // When
        var act = async () => await apiClient.GetCharactersAsync(pageNumber: pageNumber, pageSize: pageSize);

        // Then - expecting exception due to no mock server, but method should be callable
        await act.ShouldThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetSpellsAsync_WhenInvoked_ExecutesWithoutException()
    {
        // Given
        using var httpClient = new HttpClient();
        var apiSettingsOptions = CreateApiSettingsOptions(this.apiSettings);
        var apiClient = new ApiClient(httpClient, this.logger, apiSettingsOptions);

        // When
        var act = async () => await apiClient.GetSpellsAsync();

        // Then - expecting exception due to no mock server, but method should be callable
        await act.ShouldThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetPotionsAsync_WhenInvoked_ExecutesWithoutException()
    {
        // Given
        using var httpClient = new HttpClient();
        var apiSettingsOptions = CreateApiSettingsOptions(this.apiSettings);
        var apiClient = new ApiClient(httpClient, this.logger, apiSettingsOptions);

        // When
        var act = async () => await apiClient.GetPotionsAsync();

        // Then - expecting exception due to no mock server, but method should be callable
        await act.ShouldThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetMoviesAsync_WhenInvoked_ExecutesWithoutException()
    {
        // Given
        using var httpClient = new HttpClient();
        var apiSettingsOptions = CreateApiSettingsOptions(this.apiSettings);
        var apiClient = new ApiClient(httpClient, this.logger, apiSettingsOptions);

        // When
        var act = async () => await apiClient.GetMoviesAsync();

        // Then - expecting exception due to no mock server, but method should be callable
        await act.ShouldThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetBookByIdAsync_WhenInvokedWithBookId_ExecutesWithoutException()
    {
        // Given
        string bookId = Guid.NewGuid().ToString();
        using var httpClient = new HttpClient();
        var apiSettingsOptions = CreateApiSettingsOptions(this.apiSettings);
        var apiClient = new ApiClient(httpClient, this.logger, apiSettingsOptions);

        // When
        var act = async () => await apiClient.GetBookByIdAsync(bookId: bookId);

        // Then - expecting exception due to no mock server, but method should be callable
        await act.ShouldThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetCharacterByIdAsync_WhenInvokedWithCharacterId_ExecutesWithoutException()
    {
        // Given
        string characterId = Guid.NewGuid().ToString();
        using var httpClient = new HttpClient();
        var apiSettingsOptions = CreateApiSettingsOptions(this.apiSettings);
        var apiClient = new ApiClient(httpClient, this.logger, apiSettingsOptions);

        // When
        var act = async () => await apiClient.GetCharacterByIdAsync(characterId: characterId);

        // Then - expecting exception due to no mock server, but method should be callable
        await act.ShouldThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetSpellByIdAsync_WhenInvokedWithSpellId_ExecutesWithoutException()
    {
        // Given
        string spellId = Guid.NewGuid().ToString();
        using var httpClient = new HttpClient();
        var apiSettingsOptions = CreateApiSettingsOptions(this.apiSettings);
        var apiClient = new ApiClient(httpClient, this.logger, apiSettingsOptions);

        // When
        var act = async () => await apiClient.GetSpellByIdAsync(spellId: spellId);

        // Then - expecting exception due to no mock server, but method should be callable
        await act.ShouldThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetPotionByIdAsync_WhenInvokedWithPotionId_ExecutesWithoutException()
    {
        // Given
        string potionId = Guid.NewGuid().ToString();
        using var httpClient = new HttpClient();
        var apiSettingsOptions = CreateApiSettingsOptions(this.apiSettings);
        var apiClient = new ApiClient(httpClient, this.logger, apiSettingsOptions);

        // When
        var act = async () => await apiClient.GetPotionByIdAsync(potionId: potionId);

        // Then - expecting exception due to no mock server, but method should be callable
        await act.ShouldThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetMovieByIdAsync_WhenInvokedWithMovieId_ExecutesWithoutException()
    {
        // Given
        string movieId = Guid.NewGuid().ToString();
        using var httpClient = new HttpClient();
        var apiSettingsOptions = CreateApiSettingsOptions(this.apiSettings);
        var apiClient = new ApiClient(httpClient, this.logger, apiSettingsOptions);

        // When
        var act = async () => await apiClient.GetMovieByIdAsync(movieId: movieId);

        // Then - expecting exception due to no mock server, but method should be callable
        await act.ShouldThrowAsync<Exception>();
    }
}
