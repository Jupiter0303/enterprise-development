using Museum.Application.Contracts.Exhibitions;
using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Tickets;
using Museum.Application.Contracts.Visitors;
using Museum.Domain;

namespace Museum.Application.Mapping;

/// <summary>
/// Расширение для преобразования сущностей в DTO и обратно
/// </summary>
public static class Mapper
{
    /// <summary>
    /// Преобразовать сущность посетителя в DTO
    /// </summary>
    /// <param name="entity">Сущность посетителя</param>
    /// <returns>DTO посетителя</returns>
    public static VisitorDto ToDto(this Visitor entity) => new(
        entity.Id, entity.FirstName, entity.LastName, entity.MiddleName, entity.Birthday, entity.PhoneNumber);

    /// <summary>
    /// Преобразовать DTO посетителя в сущность
    /// </summary>
    /// <param name="dto">DTO посетителя</param>
    /// <returns>Сущность посетителя</returns>
    public static Visitor ToEntity(this VisitorCreateUpdateDto dto) => new()
    {
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        MiddleName = dto.MiddleName,
        Birthday = dto.Birthday,
        PhoneNumber = dto.PhoneNumber
    };

    /// <summary>
    /// Обновить сущность посетителя на основе DTO
    /// </summary>
    /// <param name="entity">Сущность посетителя</param>
    /// <param name="dto">DTO посетителя</param>
    public static void UpdateFrom(this Visitor entity, VisitorCreateUpdateDto dto)
    {
        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.MiddleName = dto.MiddleName;
        entity.Birthday = dto.Birthday;
        entity.PhoneNumber = dto.PhoneNumber;
    }

    /// <summary>
    /// Преобразовать сущность выставки в DTO
    /// </summary>
    /// <param name="entity">Сущность выставки</param>
    /// <returns>DTO выставки</returns>
    public static ExhibitionDto ToDto(this Exhibition entity) => new(
        entity.Id, entity.Name, entity.Theme, entity.HallNumber, entity.StartDate, entity.EndDate);

    /// <summary>
    /// Преобразовать DTO выставки в сущность
    /// </summary>
    /// <param name="dto">DTO выставки</param>
    /// <returns>Сущность выставки</returns>
    public static Exhibition ToEntity(this ExhibitionCreateUpdateDto dto) => new()
    {
        Name = dto.Name,
        Theme = dto.Theme,
        HallNumber = dto.HallNumber,
        StartDate = dto.StartDate,
        EndDate = dto.EndDate
    };

    /// <summary>
    /// Обновить сущность выставки на основе DTO
    /// </summary>
    /// <param name="entity">Сущность выставки</param>
    /// <param name="dto">DTO выставки</param>
    public static void UpdateFrom(this Exhibition entity, ExhibitionCreateUpdateDto dto)
    {
        entity.Name = dto.Name;
        entity.Theme = dto.Theme;
        entity.HallNumber = dto.HallNumber;
        entity.StartDate = dto.StartDate;
        entity.EndDate = dto.EndDate;
    }

    /// <summary>
    /// Преобразовать сущность экскурсии в DTO, включая список выставкок
    /// </summary>
    /// <param name="entity">Сущность экскурсии</param>
    /// <returns>DTO экскурсии</returns>
    public static ExcursionDto ToDto(this Excursion entity) => new(
        entity.Id,
        entity.StartsAt,
        entity.Duration,
        entity.Exhibitions.Select(e => e.ToDto()).ToList());

    /// <summary>
    /// Преобразовать DTO экскурсии в сущность с привязкой к найденным выставкам
    /// </summary>
    /// <param name="dto">DTO экскурсии</param>
    /// <param name="exhibitions">Список выставок</param>
    /// <returns>Сущность экскурсии</returns>
    public static Excursion ToEntity(this ExcursionCreateUpdateDto dto, List<Exhibition> exhibitions) => new()
    {
        StartsAt = dto.StartsAt,
        Duration = dto.Duration,
        Exhibitions = exhibitions
    };

    /// <summary>
    /// Обновить сущность экскурсии на основе DTO с привязкой к найденным выставкам
    /// </summary>
    /// <param name="entity">Сущность экскурсии</param>
    /// <param name="dto">DTO экскурсии</param>
    /// <param name="exhibitions">Список выставок</param>
    public static void UpdateFrom(this Excursion entity, ExcursionCreateUpdateDto dto, List<Exhibition> exhibitions)
    {
        entity.StartsAt = dto.StartsAt;
        entity.Duration = dto.Duration;
        entity.Exhibitions = exhibitions;
    }

    /// <summary>
    /// Преобразовать сущность билета в DTO, включая экскурсию и посетителя
    /// </summary>
    /// <param name="entity">Сущность билета</param>
    /// <returns></returns>
    public static TicketDto ToDto(this Ticket entity) => new(
        entity.Id,
        entity.Excursion.ToDto(),
        entity.Visitor.ToDto(),
        entity.Type,
        entity.Price);

    /// <summary>
    /// Создать сущность билета на основе DTO с привязкой к найденной экскурсии и посетителю
    /// </summary>
    /// <param name="dto">DTO билета</param>
    /// <param name="excursion">Найденная экскурсия</param>
    /// <param name="visitor">Найденный посетитель</param>
    /// <returns>Сущность билета</returns>
    public static Ticket ToEntity(this TicketCreateUpdateDto dto, Excursion excursion, Visitor visitor) => new()
    {
        Excursion = excursion,
        Visitor = visitor,
        Type = dto.Type,
        Price = dto.Price
    };

    /// <summary>
    /// Обновить сущность билета на основе DTO с привязкой к найденной экскурсии и посетителю
    /// </summary>
    /// <param name="entity">Сущность билета</param>
    /// <param name="dto">DTO билета</param>
    /// <param name="excursion">Найденная экскурсия</param>
    /// <param name="visitor">Найденный посетитель</param>
    public static void UpdateFrom(this Ticket entity, TicketCreateUpdateDto dto, Excursion excursion, Visitor visitor)
    {
        entity.Excursion = excursion;
        entity.Visitor = visitor;
        entity.Type = dto.Type;
        entity.Price = dto.Price;
    }
}