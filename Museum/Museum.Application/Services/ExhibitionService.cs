using Museum.Application.Common;
using Museum.Application.Contracts.Exhibitions;
using Museum.Application.Contracts.Excursions;
using Microsoft.Extensions.Logging;
using Museum.Application.Mapping;
using Museum.Domain;

namespace Museum.Application.Services;

public class ExhibitionService(IRepository<Exhibition> repository, IRepository<Excursion> excursionRepository, ILogger<ExhibitionService> logger)
    : CrudServiceBase<Exhibition, ExhibitionDto, ExhibitionCreateUpdateDto>(repository, logger  ), IExhibitionService
{
    protected override ExhibitionDto ToDto(Exhibition entity) => entity.ToDto();

    protected override Task<Exhibition> CreateEntity(ExhibitionCreateUpdateDto dto)
        => Task.FromResult(dto.ToEntity());

    protected override Task ApplyChanges(Exhibition entity, ExhibitionCreateUpdateDto dto)
    {
        entity.UpdateFrom(dto);
        return Task.CompletedTask;
    }
    public async Task<IList<ExcursionDto>?> GetExhibitionExcursions(int exhibitionId)
    {
        var exhibition = await repository.Read(exhibitionId);
        if (exhibition is null) return null;

        var excurions = await excursionRepository.ReadAll();
        return excurions.
                Where(exc => exc.Exhibitions.Any(exh => exh.Id == exhibition.Id))
                .Select(exc => exc.ToDto())
                .ToList();
    }
}


