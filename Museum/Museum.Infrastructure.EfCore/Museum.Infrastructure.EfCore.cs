using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Museum.Infrastructure.EfCore;

public class MuseumDbContextFactory: IDesignTimeDbContextFactory<MuseumDbContext>
{
    public MuseumDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MuseumDbContext>();
        optionsBuilder.UseMySql("Server=localhost;Database=museum;User=root;Password=;",
            new MySqlServerVersion(new Version(9, 0, 0))
        );

        return new MuseumDbContext(optionsBuilder.Options);
    }
}
