using Microsoft.AspNetCore.Mvc;
using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Exhibitions;
namespace Museum.Api.Controllers;

public class ExhibitionController(IExhibitionService service, ILogger<ExhibitionController> logger)
    : CrudControllerBase<ExhibitionDto, ExhibitionCreateUpdateDto>(service, logger)
{
    [HttpGet("{id}/excursions")]
    public async Task<ActionResult<IList<ExcursionDto>>> GetExcursions(int id)
    {
        logger.LogInformation("HTTP GET: Экскурсии выставки {Id}", id);
        var result = await service.GetExhibitionExcursions(id);
        return result == null ? NotFound() : Ok(result);
    }
}