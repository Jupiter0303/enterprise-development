using Museum.Domain.Data;

namespace Museum.Domain.Tests;

/// <summary>
/// Фикстура для инициализации тестовых данных для юнит тестов
/// </summary>
public class SeedFixture
{
    /// <summary>
    /// Список посетителей
    /// </summary>
    public List<Visitor> Visitors { get; }

    /// <summary>
    /// Список выставок
    /// </summary>
    public List<Exhibition> Exhibitions { get; }

    /// <summary>
    /// Список экскурсий
    /// </summary>
    public List<Excursion> Excursions { get; }

    /// <summary>
    /// Список билетов
    /// </summary>
    public List<Ticket> Tickets { get; }

    /// <summary>
    /// Инициализация фикстуры с тестовыми данными
    /// </summary>
    public SeedFixture()
    {
        var seedData = DataSeeder.Create();
        Visitors = seedData.Visitors;
        Exhibitions = seedData.Exhibitions;
        Excursions = seedData.Excursions;
        Tickets = seedData.Tickets;
    }
}