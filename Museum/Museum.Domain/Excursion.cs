using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Museum.Domain;

/// <summary>
/// Экскурсия
/// </summary>
public class Excursion : IEntity
{
    /// <summary>
    /// Идентификатор экскурсии
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }
    /// <summary>
    /// Дата и время начала экскурсии
    /// </summary>
    [Column("starts_at")]
    public required DateTime StartsAt { get; set; }
    /// <summary>
    /// Продолжительность экскурсии
    /// </summary>
    [Column("duration")]
    public required TimeSpan Duration { get; set; }
    /// <summary>
    /// Список выставок, включенных в экскурсию
    /// </summary>
    public required List<Exhibition> Exhibitions { get; set; }
}