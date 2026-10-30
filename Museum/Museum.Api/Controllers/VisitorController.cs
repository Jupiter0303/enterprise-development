using Museum.Application.Contracts.Visitors;

namespace Museum.Api.Controllers;

public class VisitorController(IVisitorService service, ILogger<VisitorController> logger)
    : CrudControllerBase<VisitorDto, VisitorCreateUpdateDto>(service, logger)

