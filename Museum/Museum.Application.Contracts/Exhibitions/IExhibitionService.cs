using Museum.Application.Contracts.Common;

namespace Museum.Application.Contracts.Exhibitions;

public interface IExhibitionService: ICrudService<ExhibitionDto, ExhibitionCreateUpdateDto>
{
}
