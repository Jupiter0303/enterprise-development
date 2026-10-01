namespace EnterpriseDevelopment.Domain;

/// <summary>
/// Посетитель музея
/// </summary>
public class Visitor
{
    /// <summary>
    /// Идентификатор посетителя
    /// </summary>
    public required int Id { get; init; }
    /// <summary>
    /// Имя посетителя
    /// </summary>
    public required string FirstName { get; init; }
    /// <summary>
    /// Фамилия посетителя
    /// </summary>
    public required string LastName { get; init; }
    /// <summary>
    /// Отчество посетителя
    /// </summary>
    public string? MiddleName { get; init;  }
    /// <summary>
    /// Дата рождения посетителя
    /// </summary>
    public required DateOnly Birthday { get; init; }
    /// <summary>
    /// Номер телефона посетителя
    /// </summary>
    public string? PhoneNumber { get; set; }
    
}

