namespace Museum.Domain;

/// <summary>
/// Посетитель музея
/// </summary>
public class Visitor
{
    /// <summary>
    /// Идентификатор посетителя
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Имя посетителя
    /// </summary>
    public required string FirstName { get; set; }
    /// <summary>
    /// Фамилия посетителя
    /// </summary>
    public required string LastName { get; set; }
    /// <summary>
    /// Отчество посетителя
    /// </summary>
    public string? MiddleName { get; set; }
    /// <summary>
    /// Дата рождения посетителя
    /// </summary>
    public required DateOnly Birthday { get; set; }
    /// <summary>
    /// Номер телефона посетителя
    /// </summary>
    public string? PhoneNumber { get; set; }
}