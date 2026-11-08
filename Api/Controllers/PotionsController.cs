namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PotionsController : ControllerBase
{
    private readonly IPotionsService potionsService;
    private readonly ILogger<PotionsController> logger;

    public PotionsController(IPotionsService potionsService, ILogger<PotionsController> logger)
    {
        this.potionsService = potionsService;
        this.logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PotionDto>>> GetPotions()
    {
        try
        {
            logger.LogInformation("GET /api/potions");
            List<PotionDto> potions = await potionsService.GetPotionsAsync();
            return Ok(potions);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error in GET /api/potions");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving potions" });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PotionDto>> GetPotionById(string id)
    {
        try
        {
            logger.LogInformation("GET /api/potions/{Id}", id);
            PotionDto potion = await potionsService.GetPotionByIdAsync(id);
            return Ok(potion);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error in GET /api/potions/{Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving potion" });
        }
    }
}
