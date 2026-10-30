namespace Museum.Application.Common;

public class RelatedEntityNotFoundException(string entityName, int id)
    : Exception($"{entityName} с id {id} не найден");
