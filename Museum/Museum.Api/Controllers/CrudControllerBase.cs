using Microsoft.AspNetCore.Mvc;
using Museum.Application.Contracts.Common;

namespace Museum.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto>(
    ICrudService<TDto, TCreateUpdateDto> service,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto>> logger) : ControllerBase
    where TDto : class, IHasId
{
    [HttpGet("{id}")]
    public async Task<ActionResult<TDto>> Get(int id)
    {
        logger.LogInformation("HTTP GET запрос: получение объекта {Controller} с ID {Id}", GetType().Name, id);
        var result = await service.Get(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TDto>>> GetAll()
    {
        logger.LogInformation("HTTP GET запрос: получение списка {Controller}", GetType().Name);
        var result = await service.GetAll();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TDto>> Create([FromBody] TCreateUpdateDto dto)
    {
        logger.LogInformation("HTTP POST запрос: создание объекта {Controller}", GetType().Name);
        var result = await service.Create(dto);
        return CreatedAtAction(nameof(Get), new { id = ((dynamic)result!).Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TDto>> Update(int id, [FromBody] TCreateUpdateDto dto)
    {
        logger.LogInformation("HTTP PUT запрос: обновление {Controller} с ID {Id}", GetType().Name, id);
        var result = await service.Update(id, dto);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation("HTTP DELETE запрос: удаление {Controller} с ID {Id}", GetType().Name, id);
        await service.Delete(id);
        return NoContent();
    }

}