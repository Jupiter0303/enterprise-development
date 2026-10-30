using Museum.Application.Common;
using Museum.Application.Contracts.Exhibitions;
using Museum.Application.Mapping;
using Museum.Domain;

namespace Museum.Application.Services;

public class ExhibitionService(IRepository<Exhibition> repository)
    : CrudServiceBase<Exhibition, ExhibitionDto, ExhibitionCreateUpdateDto>(repository), IExhibitionService
{
    protected override ExhibitionDto ToDto(Exhibition entity) => entity.ToDto();

    protected override Task<Exhibition> CreateEntity(ExhibitionCreateUpdateDto dto)
        => Task.FromResult(dto.ToEntity());

    protected override Task ApplyChanges(Exhibition entity, ExhibitionCreateUpdateDto dto)
    {
        entity.UpdateFrom(dto);
        return Task.CompletedTask;
    }
}


