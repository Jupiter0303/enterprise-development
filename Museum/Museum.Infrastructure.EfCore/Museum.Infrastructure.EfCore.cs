using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Museum.Infrastructure.EfCore;

/// <summary>
/// Фабрика для создания контекста базы данных в design-time 
/// </summary>
public class MuseumDbContextFactory : IDesignTimeDbContextFactory<MuseumDbContext>
{
    /// <summary>
    /// Создать контекст базы данных с жестко заданной строкой подключения и версией MySQL сервера
    /// </summary>
    /// <param name="args">Аргументы командной строки</param>
    /// <returns>Контекст базы данных</returns>
    public MuseumDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MuseumDbContext>();
        optionsBuilder.UseMySql("Server=localhost;Database=museum;User=root;Password=;",
            new MySqlServerVersion(new Version(9, 0, 0))
        );

        return new MuseumDbContext(optionsBuilder.Options);
    }
}