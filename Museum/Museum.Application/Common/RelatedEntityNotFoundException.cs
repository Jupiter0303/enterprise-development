namespace Museum.Application.Common;

/// <summary>
/// Исключение, возникаемое при попытке получить несуществующую связанную сущность
/// </summary>
/// <param name="entityName">Имя типа сущности</param>
/// <param name="id">Идентификатор сущности</param>
public class RelatedEntityNotFoundException(string entityName, int id)
    : Exception($"{entityName} с id {id} не найден");