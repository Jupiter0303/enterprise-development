using Museum.Application.Common;
using Museum.Application.Contracts.Excursions;
using Museum.Application.Mapping;
using Museum.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Museum.Application.Services;

public class ExcursionService(
    IRepository<Excursion> repository,
    IRepository<Exhibition> exhibitionRepository)
    : CrudServiceBase<Excursion, ExcursionDto, ExcursionCreateUpdateDto>(repository), IExcursionService
{
    protected override ExcursionDto ToDto(Excursion entity) => entity.ToDto();


    protected override async Task<Excursion> CreateEntity(ExcursionCreateUpdateDto dto)
    {
        var exhibitions = await LoadExhibitions(dto);
        return dto.ToEntity(exhibitions);
    }

    protected override async Task ApplyChanges(Excursion entity, ExcursionCreateUpdateDto dto)
    {
        var exhibitions = await LoadExhibitions(dto);
        entity.UpdateFrom(dto, exhibitions);


    }
    private async Task<List<Exhibition>> LoadExhibitions(ExcursionCreateUpdateDto dto)
    {
        var exhibitions = new List<Exhibition>();

        foreach (var id in dto.ExhibitionIds.Distinct())
        {
            var exhibition = await exhibitionRepository.Read(id)
                ?? throw new RelatedEntityNotFoundException(nameof(Exhibition), id);

            exhibitions.Add(exhibition);
        }

        return exhibitions;
    }

}