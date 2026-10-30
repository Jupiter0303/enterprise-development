using Museum.Application.Contracts.Excursions;
namespace Museum.Api.Controllers;

public class ExcursionController(IExcursionService service, ILogger<ExcursionController> logger)
    : CrudControllerBase<ExcursionDto, ExcursionCreateUpdateDto>(service, logger)
{ }