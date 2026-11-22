using System.Text.Json;
using System.Text.Json.Serialization;

namespace Museum.IntegrationTests;

/// <summary>
/// Настройки json-сериализации для интеграционных тестов
/// </summary>
public static class TestJson
{
    /// <summary>
    /// Параметры сериализации и десериализации json для интеграционных тестов
    /// Значения перечислений передаются через API в виде строк
    /// </summary>
    public static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };
}