using Museum.Application.Contracts.Exhibitions;
using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Tickets;
using Museum.Application.Contracts.Visitors;
using Museum.Domain;

namespace Museum.Application.Mapping;


public static class Mapper
{
    public static VisitorDto ToDto(this Visitor entity) => new(
        entity.Id, entity.FirstName, entity.LastName, entity.MiddleName, entity.Birthday, entity.PhoneNumber);

    public static Visitor ToEntity(this VisitorCreateUpdateDto dto) => new()
    {
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        MiddleName = dto.MiddleName,
        Birthday = dto.Birthday,
        PhoneNumber = dto.PhoneNumber
    };

    public static void UpdateFrom(this Visitor entity, VisitorCreateUpdateDto dto)
    {
        entity.FirstName = dto.FirstName; 
        entity.LastName = dto.LastName;
        entity.MiddleName = dto.MiddleName; 
        entity.Birthday = dto.Birthday;
        entity.PhoneNumber = dto.PhoneNumber;
    }

    public static ExhibitionDto ToDto(this Exhibition entity) => new(
        entity.Id, entity.Name, entity.Theme, entity.HallNumber, entity.StartDate, entity.EndDate);

    public static Exhibition ToEntity(this ExhibitionCreateUpdateDto dto) => new()
    {
        Name = dto.Name,
        Theme = dto.Theme,
        HallNumber = dto.HallNumber,
        StartDate = dto.StartDate,
        EndDate = dto.EndDate
    };

    public static void UpdateFrom(this Exhibition entity, ExhibitionCreateUpdateDto dto )
    {
        entity.Name = dto.Name;
        entity.Theme = dto.Theme;
        entity.HallNumber = dto.HallNumber;
        entity.StartDate = dto.StartDate;
        entity.EndDate = dto.EndDate;
    }

    public static ExcursionDto ToDto(this Excursion entity) => new(
        entity.Id,
        entity.StartsAt,
        entity.Duration,
        entity.Exhibitions.Select(e => e.ToDto()).ToList());

    public static Excursion ToEntity(this ExcursionCreateUpdateDto dto, List<Exhibition> exhibitions) => new()
    {
        StartsAt = dto.StartsAt,
        Duration = dto.Duration,
        Exhibitions = exhibitions
    };
    public static void UpdateFrom(this Excursion entity, ExcursionCreateUpdateDto dto, List<Exhibition> exhibitions)
    {
        entity.StartsAt = dto.StartsAt;
        entity.Duration = dto.Duration;
        entity.Exhibitions = exhibitions;
    }

    public static TicketDto ToDto(this Ticket entity) => new(
        entity.Id, 
        entity.Excursion.ToDto(),
        entity.Visitor.ToDto(),
        entity.Type,
        entity.Price);

    public static Ticket ToEntity(this TicketCreateUpdateDto dto, Excursion excursion, Visitor visitor) => new()
    {
        Excursion = excursion,
        Visitor = visitor,
        Type = dto.Type,
        Price = dto.Price
    };

    public static void UpdateFrom(this Ticket entity, TicketCreateUpdateDto dto, Excursion excursion, Visitor visitor)
    {
        entity.Excursion = excursion;
        entity.Visitor = visitor;
        entity.Type = dto.Type;
        entity.Price = dto.Price;
    }

}

