using Microsoft.AspNetCore.Mvc;
using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Exhibitions;
namespace Museum.Api.Controllers;

/// <summary>
/// Контроллер для запросов, связанных с выставками
/// </summary>
/// <param name="service">Сервис для работы с выставками</param>
/// <param name="logger">Логгер</param>
public class ExhibitionController(IExhibitionService service, ILogger<ExhibitionController> logger)
    : CrudControllerBase<ExhibitionDto, ExhibitionCreateUpdateDto>(service, logger)
{
    /// <summary>
    /// Получить список экскурсий выставки
    /// </summary>
    /// <param name="id">Идентификатор выставки</param>
    /// <response code="200">Список экскурсий выставки</response>
    /// <response code="404">Выставка не найдена</response>
    [HttpGet("{id}/excursions")]
    public async Task<ActionResult<IList<ExcursionDto>>> GetExcursions(int id)
    {
        logger.LogInformation("HTTP GET: Экскурсии выставки {Id}", id);
        var result = await service.GetExhibitionExcursions(id);
        return result == null ? NotFound() : Ok(result);
    }
}