using Microsoft.AspNetCore.Mvc;
using Museum.Application.Contracts.Common;

namespace Museum.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto>(
    ICrudService<TDto, TCreateUpdateDto> service,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto>> logger) : ControllerBase
{

}