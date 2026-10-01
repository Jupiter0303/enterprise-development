using System;
namespace EnterpriseDevelopment.Domain.Tests;

/// <summary>
/// Фикстура для инициализации тестовых данных для юнит тестов
/// </summary>
public class SeedFixture
{
    /// <summary>
    /// Список посетит
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
        Visitors =
        [
            new ()
            {
                Id = 1,
                FirstName = "Иван",
                LastName = "Петров",
                Birthday = new DateOnly(1990, 5, 12),
                PhoneNumber = "+79001112233"
            },
            new ()
            {
                Id = 2,
                FirstName = "Мария",
                LastName = "Сидорова",
                Birthday = new DateOnly(1985, 3, 21),
                PhoneNumber = null
            },
            new()
            {
                Id = 3,
                FirstName = "Алексей",
                LastName = "Иванов",
                Birthday = new DateOnly(2000, 7, 15),
                PhoneNumber = "+79005556677"
            },
            new()
            {
                Id = 4,
                FirstName = "Екатерина",
                LastName = "Смирнова",
                Birthday = new DateOnly(1995, 11, 30),
                PhoneNumber = null
            },
            new()
            {
                Id = 5,
                FirstName = "Дмитрий",
                LastName = "Кузнецов",
                Birthday = new DateOnly(1988, 2, 5),
                PhoneNumber = "+79008889900"
            },
            new()
            {
                Id = 6,
                FirstName = "Ольга",
                LastName = "Попова",
                Birthday = new DateOnly(1992, 9, 18),
                PhoneNumber = null
            },
            new()
            {
                Id = 7,
                FirstName = "Сергей",
                LastName = "Васильев",
                Birthday = new DateOnly(1998, 6, 10),
                PhoneNumber = "+79002223344"
            },
            new()
            {
                Id = 8,
                FirstName = "Анастасия",
                LastName = "Морозова",
                Birthday = new DateOnly(1993, 4, 25),
                PhoneNumber = null
            },
            new()
            {
                Id = 9,
                FirstName = "Николай",
                LastName = "Новиков",
                Birthday = new DateOnly(1987, 12, 1),
                PhoneNumber = "+79003334455"
            },
            new()
            {
                Id = 10,
                FirstName = "Татьяна",
                LastName = "Федорова",
                Birthday = new DateOnly(1991, 8, 14),
                PhoneNumber = null
            },
            new()
            {
                Id = 11,
                FirstName = "Владимир",
                LastName = "Соколов",
                Birthday = new DateOnly(1989, 10, 3),
                PhoneNumber = "+79004445566"
            },
            new()
            {
                Id = 12,
                FirstName = "Елена",
                LastName = "Козлова",
                Birthday = new DateOnly(1994, 1, 22),
                PhoneNumber = null
            }
        ];

        Exhibitions =
        [
            new()
            {
                Id = 1,
                Name = "Импрессионисты",
                Theme = ExhibitionTheme.Art,
                HallNumber = 101,
                StartDate = new DateOnly(2024, 1, 10),
                EndDate = new DateOnly(2024, 2, 10)
            },
            new()
            {
                Id = 2,
                Name = "Древний Египет",
                Theme = ExhibitionTheme.History,
                HallNumber = 209,
                StartDate = new DateOnly(2024, 3, 5),
                EndDate = new DateOnly(2024, 4, 5)
            },
            new()
            {
                Id = 3,
                Name = "Выставка динозавров",
                Theme = ExhibitionTheme.Science,
                HallNumber = 212,
                StartDate = new DateOnly(2024, 5, 15),
                EndDate = new DateOnly(2024, 6, 15)
            },
            new()
            {
                Id = 4,
                Name = "Фауна Северной Америки",
                Theme = ExhibitionTheme.Natural,
                HallNumber = 110,
                StartDate = new DateOnly(2024, 7, 1),
                EndDate = new DateOnly(2024, 8, 1)
            },
            new()
            {
                Id = 5,
                Name = "Русский авангард",
                Theme = ExhibitionTheme.Art,
                HallNumber = 103,
                StartDate = new DateOnly(2024, 9, 10),
                EndDate = new DateOnly(2024, 10, 10)
            },
            new()
            {
                Id = 6,
                Name = "Римская империя",
                Theme = ExhibitionTheme.History,
                HallNumber = 206,
                StartDate = new DateOnly(2024, 11, 20),
                EndDate = new DateOnly(2024, 12, 20)
            },
            new()
            {
                Id = 7,
                Name = "Космос",
                Theme = ExhibitionTheme.Science,
                HallNumber = 211,
                StartDate = new DateOnly(2024, 1, 25),
                EndDate = new DateOnly(2024, 2, 25)
            },
            new()
            {
                Id = 8,
                Name = "Тропические леса",
                Theme = ExhibitionTheme.Natural,
                HallNumber = 108,
                StartDate = new DateOnly(2024, 3, 15),
                EndDate = new DateOnly(2024, 4, 15)
            },
            new()
            {
                Id = 9,
                Name = "Выставка архитектуры",
                Theme = ExhibitionTheme.Art,
                HallNumber = 103,
                StartDate = new DateOnly(2024, 5, 5),
                EndDate = new DateOnly(2024, 6, 5)
            },
            new()
            {
                Id = 10,
                Name = "Выставка древних цивилизаций",
                Theme = ExhibitionTheme.History,
                HallNumber = 200,
                StartDate = new DateOnly(2024, 7, 20),
                EndDate = new DateOnly(2024, 8, 20)
            }
        ];

        Excursions =
        [
            new()
            {
                Id = 1,
                Date = new DateOnly(2024, 1, 15),
                StartTime = new TimeOnly(10, 0),
                Duration = new TimeSpan(1, 30, 0),
                Exhibitions = [Exhibitions[0], Exhibitions[3], Exhibitions[4]]
            },
            new()
            {
                Id = 2, Date = new DateOnly(2024, 3, 10),
                StartTime = new TimeOnly(14, 0),
                Duration = new TimeSpan(2, 0, 0),
                Exhibitions = [Exhibitions[1], Exhibitions[4], Exhibitions[5]]
            },
            new()
            {
                Id = 3,
                Date = new DateOnly(2024, 5, 20),
                StartTime = new TimeOnly(11, 30),
                Duration = new TimeSpan(1, 45, 0),
                Exhibitions = [Exhibitions[2], Exhibitions[4], Exhibitions[8]]
            },
            new()
            {
                Id = 4,
                Date = new DateOnly(2024, 7, 5),
                StartTime = new TimeOnly(9, 0),
                Duration = new TimeSpan(2, 10, 0),
                Exhibitions = [Exhibitions[0], Exhibitions[8], Exhibitions[9]]
            },
            new() {
                Id = 5,
                Date = new DateOnly(2024, 9, 15),
                StartTime = new TimeOnly(13, 0),
                Duration = new TimeSpan(1, 30, 0),
                Exhibitions = [Exhibitions[0], Exhibitions[1], Exhibitions[5]]
            },
            new()
            {
                Id = 6,
                Date = new DateOnly(2024, 11, 10),
                StartTime = new TimeOnly(15, 30),
                Duration = new TimeSpan(2, 50, 0),
                Exhibitions = [ Exhibitions[0], Exhibitions[6]]
            },
            new()
            {
                Id = 7,
                Date = new DateOnly(2024, 2, 5),
                StartTime = new TimeOnly(10, 0),
                Duration = new TimeSpan(1, 30, 0),
                Exhibitions = [Exhibitions[0], Exhibitions[1], Exhibitions[6]]
            },
            new() 
            {
                Id = 8,
                Date = new DateOnly(2024, 4, 10),
                StartTime = new TimeOnly(14, 0),
                Duration = new TimeSpan(2, 0, 0),
                Exhibitions = [Exhibitions[4], Exhibitions[6]]
            },
            new()
            {
                Id = 9,
                Date = new DateOnly(2024, 6, 20),
                StartTime = new TimeOnly(11, 30),
                Duration = new TimeSpan(1, 45, 0),
                Exhibitions = [Exhibitions[0]]
            },
            new()
            {
                Id = 10,
                Date = new DateOnly(2024, 8, 5),
                StartTime = new TimeOnly(9, 0),
                Duration = new TimeSpan(2, 15, 0),
                Exhibitions = [Exhibitions[0], Exhibitions[1], Exhibitions[8]]
            },
            new() 
            {
                Id = 11,
                Date = new DateOnly(2024, 10, 15),
                StartTime = new TimeOnly(13, 0),
                Duration = new TimeSpan(1, 30, 0),
                Exhibitions = [Exhibitions[4]]
            }
        ];

        Tickets =
        [
            new()
            {
                Id = 1,
                Excursion = Excursions[0],
                Visitor = Visitors[0],
                Type = TicketType.Adult,
                Price = 500m
            },
            new() 
            {
                Id = 2,
                Excursion = Excursions[1],
                Visitor = Visitors[1],
                Type = TicketType.Discounted,
                Price = 300m
            },
            new() 
            {
                Id = 3,
                Excursion = Excursions[4],
                Visitor = Visitors[2],
                Type = TicketType.Adult,
                Price = 600m
            },
            new()
            {
                Id = 4,
                Excursion = Excursions[6],
                Visitor = Visitors[3],
                Type = TicketType.Discounted,
                Price = 400m
            },
            new()
            {
                Id = 5,
                Excursion = Excursions[7],
                Visitor = Visitors[4],
                Type = TicketType.Adult,
                Price = 550m
            },
            new() 
            {
                Id = 6,
                Excursion = Excursions[0],
                Visitor = Visitors[5],
                Type = TicketType.Discounted,
                Price = 350m
            },
            new()
            {
                Id = 7,
                Excursion = Excursions[1],
                Visitor = Visitors[6],
                Type = TicketType.Adult,
                Price = 500m
            },
            new()
            {
                Id = 8,
                Excursion = Excursions[2],
                Visitor = Visitors[7],
                Type = TicketType.Discounted,
                Price = 300m
            },
            new()
            {
                Id = 9,
                Excursion = Excursions[3],
                Visitor = Visitors[8],
                Type = TicketType.Adult,
                Price = 600m
            },
            new()
            {
                Id = 10,
                Excursion = Excursions[7],
                Visitor = Visitors[9],
                Type = TicketType.Discounted,
                Price = 400m
            },
            new() 
            {
                Id = 11,
                Excursion = Excursions[5],
                Visitor = Visitors[10],
                Type = TicketType.Adult,
                Price = 550m
            },
            new() 
            {
                Id = 12,
                Excursion = Excursions[9],
                Visitor = Visitors[11],
                Type = TicketType.Discounted,
                Price = 350m
            },
            new() 
            {
                Id = 13,
                Excursion = Excursions[10],
                Visitor = Visitors[0],
                Type = TicketType.Adult,
                Price = 500m
            },
            new()
            {
                Id = 14,
                Excursion = Excursions[10],
                Visitor = Visitors[1],
                Type = TicketType.Discounted,
                Price = 300m
            }
        ];
    }
}
