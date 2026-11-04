using Microsoft.AspNetCore.Mvc;
using Museum.Application.Contracts.Common;

namespace Museum.Api.Controllers;

/// <summary>
/// Базовый контроллер для CRUD операций
/// </summary>
[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto>(
    ICrudService<TDto, TCreateUpdateDto> service,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto>> logger) : ControllerBase
    where TDto : class, IHasId
{
    /// <summary>
    /// Получить объект по id 
    /// </summary>
    /// <param name="id">идентификатор объекта</param>
    /// <response code="200">Объект найден</response>
    /// <response code="404">Объект не найден</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<TDto>> Get(int id)
    {
        logger.LogInformation("HTTP GET запрос: получение объекта {Controller} с ID {Id}", GetType().Name, id);
        var result = await service.Get(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Получить список всех объектов
    /// </summary>
    /// <response code="200">Список объектов найден</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TDto>>> GetAll()
    {
        logger.LogInformation("HTTP GET запрос: получение списка {Controller}", GetType().Name);
        var result = await service.GetAll();
        return Ok(result);
    }

    /// <summary>
    /// Создать новый объект
    /// </summary>
    /// <response code="201">Объект создан</response>
    /// <response code="400">Некорректные данные</response>
    [HttpPost]
    public async Task<ActionResult<TDto>> Create([FromBody] TCreateUpdateDto dto)
    {
        logger.LogInformation("HTTP POST запрос: создание нового объекта {Controller}", GetType().Name);
        var result = await service.Create(dto);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>
    /// Обновить существующий объект
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <param name="dto">Данные для обновления</param>
    /// <response code="200">Объект обновлен</response>
    /// <response code="400">Невалидные данные</response>
    /// <response code="404">Объект не найден</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<TDto>> Update(int id, [FromBody] TCreateUpdateDto dto)
    {
        logger.LogInformation("HTTP PUT запрос: обновление {Controller} с ID {Id}", GetType().Name, id);
        var result = await service.Update(id, dto);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Удалить обхект
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <response code="204">Объект удален</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation("HTTP DELETE запрос: удаление {Controller} с ID {Id}", GetType().Name, id);
        await service.Delete(id);
        return NoContent();
    }
}