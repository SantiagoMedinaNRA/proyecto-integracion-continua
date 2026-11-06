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
    public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCharacters([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
    {
        try
        {
            logger.LogInformation("GET /api/characters?pageNumber={PageNumber}&pageSize={PageSize}", pageNumber, pageSize);
            List<CharacterDto> characters = await charactersService.GetCharactersAsync(pageNumber, pageSize);
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
    public async Task<ActionResult<CharacterDto>> GetCharacterById(string id)
    {
        try
        {
            logger.LogInformation("GET /api/characters/{Id}", id);
            CharacterDto character = await charactersService.GetCharacterByIdAsync(id);
            return Ok(character);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error in GET /api/characters/{Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving character" });
        }
    }
}
