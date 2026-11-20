using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Museum.Shared.Enums;

namespace Museum.Domain;

/// <summary>
/// Выставка
/// </summary>
[Table("exhibitions")]
public class Exhibition: IEntity
{
    /// <summary>
    /// Идентификатор выставки
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }
    /// <summary>
    /// Название выставки
    /// </summary>
    [Column("name")]
    public required string Name { get; set; }
    /// <summary>
    /// Тема выставки
    /// </summary>
    [Column("theme")]
    public required ExhibitionTheme Theme { get; set; }
    /// <summary>
    /// Номер зала
    /// </summary>
    [Column("hall_number")]
    public required int HallNumber { get; set; }
    /// <summary>
    /// Дата начала выставки
    /// </summary>
    [Column("start_date")]
    public required DateOnly StartDate { get; set; }
    /// <summary>
    /// Дата окончания выставки
    /// </summary>
    [Column("end_date")]
    public required DateOnly EndDate { get; set; }
}