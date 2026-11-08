namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpellsController : ControllerBase
{
    private readonly ISpellsService spellsService;
    private readonly ILogger<SpellsController> logger;

    public SpellsController(ISpellsService spellsService, ILogger<SpellsController> logger)
    {
        this.spellsService = spellsService;
        this.logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<SpellDto>>> GetSpells()
    {
        try
        {
            logger.LogInformation("GET /api/spells");
            List<SpellDto> spells = await spellsService.GetSpellsAsync();
            return Ok(spells);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error in GET /api/spells");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving spells" });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpellDto>> GetSpellById(string id)
    {
        try
        {
            logger.LogInformation("GET /api/spells/{Id}", id);
            SpellDto spell = await spellsService.GetSpellByIdAsync(id);
            return Ok(spell);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error in GET /api/spells/{Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving spell" });
        }
    }
}
