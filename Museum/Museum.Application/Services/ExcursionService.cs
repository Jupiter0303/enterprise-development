using Museum.Application.Common;
using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Exhibitions;
using Museum.Application.Contracts.Tickets;
using Museum.Application.Mapping;
using Museum.Domain;
using Microsoft.Extensions.Logging;

namespace Museum.Application.Services;

public class ExcursionService(
    IRepository<Excursion> repository,
    IRepository<Exhibition> exhibitionRepository,
    IRepository<Ticket> ticketRepository,
    ILogger<ExcursionService> logger)
    : CrudServiceBase<Excursion, ExcursionDto, ExcursionCreateUpdateDto>(repository, logger), IExcursionService
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
    public async Task<IList<TicketDto>?> GetExcursionTickets(int excursionId)
    {
        var excursion = await repository.Read(excursionId);
        if (excursion is null)
            return null;

        var tickets = await ticketRepository.ReadAll();
        return tickets
            .Where(t => t.Excursion.Id == excursionId)
            .Select(t => t.ToDto())
            .ToList();
    }
    public async Task<IList<ExhibitionDto>?> GetExcursionExhibitions(int excursionId)
    {
        var excursion = await repository.Read(excursionId);
        if (excursion is null)
            return null;

        return excursion.Exhibitions.Select(e => e.ToDto()).ToList();
    }

}