using Museum.Application.Contracts.Common;

namespace Museum.Application.Contracts.Excursions;

public interface IExcursionService : ICrudService<ExcursionDto, ExcursionCreateUpdateDto>
{
}
