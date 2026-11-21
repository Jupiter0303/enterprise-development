# Лабораторная работа №3 — «Сервер»

**Студент:** Лошманкин Александр  
**Группа:** 6411  
**Вариант:** 11 Предметная область: Музей 
**Стек:** .NET 10, ASP.NET Core Web API, Entity Framework Core, MySQL, .NET Aspire, Docker, Scalar (OpenAPI).

## Описание проекта

Реализация серверного приложения REST API для предметной области «Музей». 
В рамках третьей лабораторной работы приложение переведено с хранения данных в оперативной памяти на полноценную реляционную базу данных **MySQL**. Управление схемой БД и начальное заполнение реализованы через миграции **Entity Framework Core**. Развёртывание базы данных и самого API автоматизировано с помощью оркестратора **.NET Aspire**.

### Архитектура и слои

Архитектура построена по принципам Чистой Архитектуры:

- `Museum.Domain` — доменные сущности (`Visitor`, `Exhibition`, `Excursion`, `Ticket`), интерфейсы и генератор тестовых данных (`DataSeeder`).
- `Museum.Application.Contracts` — DTO и интерфейсы сервисов.
- `Museum.Application` — бизнес-логика (сервисы, маппинг, аналитика).
- `Museum.Infrastructure.InMemory` — альтернативная реализация репозиториев для хранения данных в оперативной памяти.
- **`Museum.Infrastructure.EfCore` — новый слой инфраструктуры.** Содержит `MuseumDbContext`, реализацию репозиториев через EF Core, design-time фабрику и миграции.
- `Museum.Api` — контроллеры, обработка исключений и настройка DI.
- **`Museum.AppHost` и `Museum.ServiceDefaults` — проекты Aspire.** Отвечают за оркестрацию (запуск Docker-контейнера с MySQL), телеметрию и health checks.

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

## Запуск приложения

### Предварительные требования

- **.NET 10 SDK**
- **Docker Desktop** (должен быть запущен для работы MySQL)


### Способ 1: Запуск через Aspire

Перейдите в корень решения и выполните:

```sh
dotnet restore Museum.slnx
dotnet run --project Museum.AppHost

```

1. Aspire автоматически скачает образ MySQL, поднимет контейнер и запустит API.
2. В консоли появится ссылка на **Aspire Dashboard**. Откройте её.
3. В дашборде дождитесь, пока все ресурсы (`mysql`, `api`) станут зелёными (`Running`).
4. Перейдите по ссылке на ресурс `api`, добавьте в конце URL `/scalar` — откроется интерфейс Scalar для тестирования API.

### Способ 2: Запуск в режиме In-Memory

Если Docker недоступен, можно запустить API без базы данных. В файле `Museum.Api/appsettings.json` измените параметр:

```json
"Storage": {
  "Provider": "InMemory"
}
```
Затем запустите API напрямую (без Aspire):

```
dotnet run --project Museum/Museum.Api
```

## Тесты
Для запуска юнит-тестов выполните из корня репозитория:

```sh
dotnet test Museum.slnx

```
