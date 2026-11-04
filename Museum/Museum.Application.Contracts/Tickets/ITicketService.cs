using Museum.Application.Contracts.Common;


namespace Museum.Application.Contracts.Tickets;

/// <summary>
/// Интерфейс сервиса для работы с билетами
/// </summary>
public interface ITicketService: ICrudService<TicketDto, TicketCreateUpdateDto>
{
}
