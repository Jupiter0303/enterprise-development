using Museum.Application.Contracts.Common;

namespace Museum.Application.Contracts.Visitors;

/// <summary>
/// DTO для передачи данных о посетителе
/// </summary>
/// <param name="Id">Идентификатор посетителя</param>
/// <param name="FirstName">Имя посетителя</param>
/// <param name="LastName">Фамилия посетителя</param>
/// <param name="MiddleName">Отчество посетителя</param>
/// <param name="Birthday">День рождения посетителя</param>
/// <param name="PhoneNumber">Номер телефона посетителя</param>
public record VisitorDto
(
    
    int Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly Birthday,
    string? PhoneNumber
) : IHasId;