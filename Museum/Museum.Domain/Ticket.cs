using Museum.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Museum.Domain;

/// <summary>
/// Билет на экскурсию
/// </summary>
[Table("tickets")]
public class Ticket : IEntity
{
    /// <summary>
    /// Идентификатор билета
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }
    /// <summary>
    /// Экскурсия, на которую оформлен билет
    /// </summary>
    public required Excursion Excursion { get; set; }
    /// <summary>
    /// Посетитель, купивший билет
    /// </summary>
    public required Visitor Visitor { get; set; }
    /// <summary>
    /// Тип билета
    /// </summary>
    [Column("type")]
    public required TicketType Type { get; set; }
    /// <summary>
    /// Цена билета
    /// </summary>
    [Column("price", TypeName = "decimal(10, 2)")]
    public required decimal Price { get; set; }
}