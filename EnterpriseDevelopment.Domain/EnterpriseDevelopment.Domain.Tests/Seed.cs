using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EnterpriseDevelopment.Domain.Tests;

public class Seed
{
    public static List<Visitor> GetVisitors() =>
        [
        new() {Id = 1, FirstName = "Иван", LastName = "Петров", Birthday = new DateOnly(1990, 5, 12), PhoneNumber = "+79001112233"},
        new() {Id = 2, FirstName = "Мария", LastName = "Сидорова", Birthday = new DateOnly(1985, 3, 21), PhoneNumber = null},
        new() { Id = 3,FirstName  = "Алексей", LastName = "Иванов", Birthday = new DateOnly(2000, 7, 15), PhoneNumber = "+79005556677" },
        new() { Id = 4,FirstName  = "Екатерина", LastName = "Смирнова", Birthday = new DateOnly(1995, 11, 30), PhoneNumber = null },
        new() { Id = 5,FirstName  = "Дмитрий", LastName = "Кузнецов", Birthday = new DateOnly(1988, 2, 5), PhoneNumber = "+79008889900" },
        new(){ Id = 6, FirstName = "Ольга", LastName = "Попова", Birthday = new DateOnly(1992, 9, 18), PhoneNumber = null },
        new() { Id = 7, FirstName = "Сергей", LastName = "Васильев", Birthday = new DateOnly(1998, 6, 10), PhoneNumber = "+79002223344" },
        new(){ Id = 8, FirstName = "Анастасия", LastName = "Морозова", Birthday = new DateOnly(1993, 4, 25), PhoneNumber = null },
        new() { Id = 9, FirstName = "Николай", LastName = "Новиков", Birthday = new DateOnly(1987, 12, 1), PhoneNumber = "+79003334455" },
        new() { Id = 10, FirstName = "Татьяна", LastName = "Федорова", Birthday = new DateOnly(1991, 8, 14), PhoneNumber = null }
        ];

    public static List<Ticket> C(List<Excursion> excursions, List<Visitor> visitors) =>
        [
        new() { Id = 1, Excursion = excursions[0], Visitor = visitors[0], Type = TicketType.Standard, Price = 500 },
        new() { Id = 2, Excursion = excursions[1], Visitor = visitors[1], Type = TicketType.Student, Price = 300 },
        new() { Id = 3, Excursion = excursions[2], Visitor = visitors[2], Type = TicketType.VIP, Price = 1000 }
        ];
    public static List<Excursion> GetExcursions(List<Exhibition> exhibitions) =>
        [
        new() { Id = 1, Date = new DateOnly(2024, 1, 15), StartTime = new TimeOnly(10, 0), Duration = new TimeSpan(1, 30, 0), Exhibitions = exhibitions },
        new() { Id = 2, Date = new DateOnly(2024, 3, 10), StartTime = new TimeOnly(14, 0), Duration = new TimeSpan(2, 0, 0), Exhibitions = exhibitions },
        new() { Id = 3, Date = new DateOnly(2024, 5, 20), StartTime = new TimeOnly(11, 30), Duration = new TimeSpan(1, 45, 0), Exhibitions = exhibitions }
        ];
    public static List<Exhibition> GetExhibitions() =>
        [
        new() { Id = 1, Name = "Выставка живописи", Theme = ExhibitionTheme.Art, HallNumber = 1, StartDate = new DateOnly(2024, 1, 10), EndDate = new DateOnly(2024, 2, 10) },
        new() { Id = 2, Name = "Выставка скульптуры", Theme = ExhibitionTheme.Sculpture, HallNumber = 2, StartDate = new DateOnly(2024, 3, 5), EndDate = new DateOnly(2024, 4, 5) },
        new() { Id = 3, Name = "Выставка фотографии", Theme = ExhibitionTheme.Photography, HallNumber = 3, StartDate = new DateOnly(2024, 5, 15), EndDate = new DateOnly(2024, 6, 15) }
        ];>
}
