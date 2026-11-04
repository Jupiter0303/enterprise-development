# Лабораторная работа №2 — «Сервер»

**Студент:** Лошманкин Александр  
**Группа:** 6411  
**Вариант:** 11 Предметная область: Музей 
**Стек:** .NET 10, ASP.NET Core Web API, xUnit v3, Scalar (OpenAPI)

## Описание проекта

Реализация серверного приложения REST API для предметной области «Музей». 
Хранение данных на данном этапе осуществляется в оперативной памяти и возвращается к исходному набору после перезапуска сервера.

Архитектура построена по принципам Чистой Архитектуры:

- `Museum.Domain` — доменные сущности (`Visitor`, `Exhibition`, `Excursion`, `Ticket`) и интерфейсы (`IRepository`, `IEntity`).
- `Museum.Application.Contracts` — DTO и интерфейсы сервисов.
- `Museum.Application` — бизнес-логика (сервисы, маппинг, аналитика).
- `Museum.Infrastructure.InMemory` — реализация репозиториев и генерация тестовых данных (`DataSeeder`).
- `Museum.Api` — контроллеры, обработка исключений и настройка Dependency Injection.

## Реализованный функционал

### 1. CRUD-операции
Для сущностей `Visitor`, `Exhibition`, `Excursion` и `Ticket` реализованы базовые операции:
- `GET /api/{resource}` — получение списка (200).
- `GET /api/{resource}/{id}` — получение по ID (200, 404).
- `POST /api/{resource}` — создание (201).
- `PUT /api/{resource}/{id}` — обновление (200, 400, 404).
- `DELETE /api/{resource}/{id}` — удаление (204).

Также реализованы **маршруты для связанных коллекций**:
- `GET /api/Visitor/{id}/tickets` — билеты посетителя.
- `GET /api/Excursion/{id}/tickets` — билеты экскурсии.
- `GET /api/Excursion/{id}/exhibitions` — выставки в рамках экскурсии.
- `GET /api/Exhibition/{id}/excursions` — экскурсии, посещающие выставку.

### 2. Аналитические запросы
В `AnalyticsController` вынесены 5 запросов из ЛР1:

| Запрос | HTTP-метод |
| --- | --- |
| Топ-5 выставок по уникальным посетителям | `GET /api/Analytics/top-exhibitions` |
| Экскурсии с минимальным количеством участников | `GET /api/Analytics/min-visitors-excursions` |
| Сводная статистика по темам за период | `GET /api/Analytics/theme-statistics?startDate=2024-01-01&endDate=2024-12-31` |
| Экскурсии в зале за период | `GET /api/Analytics/excursions-in-hall?hallNumber=101&startDate=2024-01-01&endDate=2024-12-31` |
| Посетители экскурсии (сортировка по ФИО) | `GET /api/Analytics/visitors-by-excursion/1` |

### 3. Валидация и обработка ошибок
- Валидация входящих DTO (DataAnnotations, `IValidatableObject`, проверка `EnumDataType`).
- Глобальный обработчик исключений (`RelatedEntityExceptionHandler`) возвращает **400 Bad Request** с описанием, если передан ID несуществующей связанной сущности (например, при создании билета на несуществующую экскурсию).
- Логирование работы контроллеров и служб.

## Запуск

```sh
dotnet restore Museum.slnx
dotnet run --project Museum/Museum.Api

```

## Тесты
```sh
dotnet test Museum.slnx

```