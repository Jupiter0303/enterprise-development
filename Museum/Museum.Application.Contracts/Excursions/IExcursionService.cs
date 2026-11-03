using Museum.Application.Contracts.Common;
using Museum.Application.Contracts.Exhibitions;
using Museum.Application.Contracts.Tickets;

namespace Museum.Application.Contracts.Excursions;

public interface IExcursionService : ICrudService<ExcursionDto, ExcursionCreateUpdateDto>
{
    public Task<IList<TicketDto>?> GetExcursionTickets(int excursionId);
    public Task<IList<ExhibitionDto>?> GetExcursionExhibitions(int excursionId);

}
