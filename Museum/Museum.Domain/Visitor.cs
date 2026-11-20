using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Museum.Domain;

/// <summary>
/// Посетитель музея
/// </summary>
[Table("visitors")]
public class Visitor : IEntity
{
    /// <summary>
    /// Идентификатор посетителя
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }
    /// <summary>
    /// Имя посетителя
    /// </summary>
    [Column("first_name")]
    public required string FirstName { get; set; }
    /// <summary>
    /// Фамилия посетителя
    /// </summary>
    [Column("last_name")]
    public required string LastName { get; set; }
    /// <summary>
    /// Отчество посетителя
    /// </summary>
    [Column("middle_name")]
    public string? MiddleName { get; set; }
    /// <summary>
    /// Дата рождения посетителя
    /// </summary>
    [Column("birthday")]
    public required DateOnly Birthday { get; set; }
    /// <summary>
    /// Номер телефона посетителя
    /// </summary>
    [Column("phone_number")]
    public string? PhoneNumber { get; set; }
}