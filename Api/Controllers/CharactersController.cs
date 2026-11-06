namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly ICharactersService charactersService;
    private readonly ILogger<CharactersController> logger;

    public CharactersController(ICharactersService charactersService, ILogger<CharactersController> logger)
    {
        this.charactersService = charactersService;
        this.logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CharacterResponse>>> GetCharacters()
    {
        try
        {
            logger.LogInformation("GET /api/characters");
            List<CharacterResponse> characters = await charactersService.GetCharactersAsync();
            return Ok(characters);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error in GET /api/characters");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving characters" });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CharacterResponse>> GetCharacterById(string id)
    {
        try
        {
            logger.LogInformation("GET /api/characters/{Id}", id);
            CharacterResponse character = await charactersService.GetCharacterByIdAsync(id);
            return Ok(character);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error in GET /api/characters/{Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving character" });
        }
    }
}
