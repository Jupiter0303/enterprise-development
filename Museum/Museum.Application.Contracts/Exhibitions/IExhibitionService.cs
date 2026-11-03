using Museum.Application.Contracts.Common;
using Museum.Application.Contracts.Excursions;

namespace Museum.Application.Contracts.Exhibitions;

public interface IExhibitionService: ICrudService<ExhibitionDto, ExhibitionCreateUpdateDto>
{
    public Task<IList<ExcursionDto>?> GetExhibitionExcursions(int exhibitionId);
}
