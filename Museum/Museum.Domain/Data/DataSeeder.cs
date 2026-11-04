namespace Museum.Domain.Data;

/// <summary>
/// контейнер для передачи списков сущностей
/// </summary>
public record SeedData
{
    public required List<Exhibition> Exhibitions { get; init; }
    public required List<Excursion> Excursions { get; init; }
    public required List<Visitor> Visitors { get; init; }
    public required List<Ticket> Tickets { get; init; }
}

/// <summary>
/// класс-генератор стартовых данных
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// создает и возвращает набор стартовых данных
    /// </summary>
    /// <returns>Набор стартовых данных в виде SeedData</returns>
    public static SeedData Create()
    {
        var visitors = new List<Visitor>
        {
            new()
            {
                Id = 1,
                FirstName = "Иван",
                LastName = "Петров",
                Birthday = new DateOnly(1990, 5, 12),
                PhoneNumber = "+79001112233"
            },
            new()
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
        };

        var exhibitions = new List<Exhibition>
        {
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
        };
        var excursions = new List<Excursion>
        {
            new()
            {
                Id = 1,
                StartsAt = new DateTime(2024, 1, 15, 10, 0, 0),
                Duration = new TimeSpan(1, 30, 0),
                Exhibitions = [exhibitions[0], exhibitions[3], exhibitions[4]]
            },
            new()
            {
                Id = 2,
                StartsAt = new DateTime(2024, 3, 10, 14, 0, 0),
                Duration = new TimeSpan(2, 0, 0),
                Exhibitions = [exhibitions[1], exhibitions[4], exhibitions[5]]
            },
            new()
            {
                Id = 3,
                StartsAt = new DateTime(2024, 5, 20, 11, 30, 0),
                Duration = new TimeSpan(1, 45, 0),
                Exhibitions = [exhibitions[2], exhibitions[4], exhibitions[8]]
            },
            new()
            {
                Id = 4,
                StartsAt = new DateTime(2024, 7, 5, 9, 0, 0),
                Duration = new TimeSpan(2, 10, 0),
                Exhibitions = [exhibitions[0], exhibitions[8], exhibitions[9]]
            },
            new()
            {
                Id = 5,
                StartsAt = new DateTime(2024, 9, 15, 13, 0, 0),
                Duration = new TimeSpan(1, 30, 0),
                Exhibitions = [exhibitions[0], exhibitions[1], exhibitions[5]]
            },
            new()
            {
                Id = 6,
                StartsAt = new DateTime(2024, 11, 10, 15, 30, 0),
                Duration = new TimeSpan(2, 50, 0),
                Exhibitions = [exhibitions[0], exhibitions[6]]
            },
            new()
            {
                Id = 7,
                StartsAt = new DateTime(2024, 2, 5, 10, 0, 0),
                Duration = new TimeSpan(1, 30, 0),
                Exhibitions = [exhibitions[0], exhibitions[1], exhibitions[6]]
            },
            new()
            {
                Id = 8,
                StartsAt = new DateTime(2024, 4, 10, 14, 0, 0),
                Duration = new TimeSpan(2, 0, 0),
                Exhibitions = [exhibitions[4], exhibitions[6]]
            },
            new()
            {
                Id = 9,
                StartsAt = new DateTime(2024, 6, 20, 11, 30, 0),
                Duration = new TimeSpan(1, 45, 0),
                Exhibitions = [exhibitions[0]]
            },
            new()
            {
                Id = 10,
                StartsAt = new DateTime(2024, 8, 5, 9, 0, 0),
                Duration = new TimeSpan(2, 15, 0),
                Exhibitions = [exhibitions[0], exhibitions[1], exhibitions[8]]
            },
            new()
            {
                Id = 11,
                StartsAt = new DateTime(2024, 10, 15, 13, 0, 0),
                Duration = new TimeSpan(1, 30, 0),
                Exhibitions = [exhibitions[4]]
            }
        };

        var tickets = new List<Ticket>
        {
            new()
            {
                Id = 1,
                Excursion = excursions[0],
                Visitor = visitors[0],
                Type = TicketType.Adult,
                Price = 500m
            },
            new()
            {
                Id = 2,
                Excursion = excursions[1],
                Visitor = visitors[1],
                Type = TicketType.Discounted,
                Price = 300m
            },
            new()
            {
                Id = 3,
                Excursion = excursions[4],
                Visitor = visitors[2],
                Type = TicketType.Adult,
                Price = 600m
            },
            new()
            {
                Id = 4,
                Excursion = excursions[6],
                Visitor = visitors[3],
                Type = TicketType.Discounted,
                Price = 400m
            },
            new()
            {
                Id = 5,
                Excursion = excursions[7],
                Visitor = visitors[4],
                Type = TicketType.Adult,
                Price = 550m
            },
            new()
            {
                Id = 6,
                Excursion = excursions[0],
                Visitor = visitors[5],
                Type = TicketType.Discounted,
                Price = 350m
            },
            new()
            {
                Id = 7,
                Excursion = excursions[1],
                Visitor = visitors[6],
                Type = TicketType.Adult,
                Price = 500m
            },
            new()
            {
                Id = 8,
                Excursion = excursions[2],
                Visitor = visitors[7],
                Type = TicketType.Discounted,
                Price = 300m
            },
            new()
            {
                Id = 9,
                Excursion = excursions[3],
                Visitor = visitors[8],
                Type = TicketType.Adult,
                Price = 600m
            },
            new()
            {
                Id = 10,
                Excursion = excursions[7],
                Visitor = visitors[9],
                Type = TicketType.Discounted,
                Price = 400m
            },
            new()
            {
                Id = 11,
                Excursion = excursions[5],
                Visitor = visitors[10],
                Type = TicketType.Adult,
                Price = 550m
            },
            new()
            {
                Id = 12,
                Excursion = excursions[9],
                Visitor = visitors[11],
                Type = TicketType.Discounted,
                Price = 350m
            },
            new()
            {
                Id = 13,
                Excursion = excursions[10],
                Visitor = visitors[0],
                Type = TicketType.Adult,
                Price = 500m
            },
            new()
            {
                Id = 14,
                Excursion = excursions[10],
                Visitor = visitors[1],
                Type = TicketType.Discounted,
                Price = 300m
            }
        };

        return new SeedData
        {
            Exhibitions = exhibitions,
            Excursions = excursions,
            Visitors = visitors,
            Tickets = tickets
        };
    }
}

