using Museum.Application.Contracts.Common;
using Museum.Application.Contracts.Tickets;

namespace Museum.Application.Contracts.Visitors;

/// <summary>
/// Интерфейс сервиса для работы с посетителями
/// </summary>
public interface IVisitorService : ICrudService<VisitorDto, VisitorCreateUpdateDto>
{
    /// <summary>
    /// Вернуть список билетов посетителя
    /// </summary>
    /// <param name="visitorId">Идентификатор посетителя</param>
    /// <returns>Список билетов посетителя</returns>
    public Task<IList<TicketDto>?> GetVisitorTickets(int visitorId);
}