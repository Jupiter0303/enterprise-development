using Museum.Application.Contracts.Exhibitions;
namespace Museum.Api.Controllers;

public class ExhibitionController(IExhibitionService service, ILogger<ExhibitionController> logger)
    : CrudControllerBase<ExhibitionDto, ExhibitionCreateUpdateDto>(service, logger)
{ }