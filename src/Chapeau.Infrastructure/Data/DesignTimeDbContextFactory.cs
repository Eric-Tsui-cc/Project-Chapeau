using  Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Chapeau.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ChapeauDbContext>
{
    public ChapeauDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<ChapeauDbContext>();

        if (args.Contains("--postgres"))
        {
            var connection = Environment.GetEnvironmentVariable(
                "ConnectionStrings__Postgres");

            if (string.IsNullOrWhiteSpace(connection))
            {
                throw new InvalidOperationException(
                    "Set ConnectionStrings__Postgres before using PostgreSQL.");
            }

            options.UseNpgsql(
                connection,
                postgres => postgres.MigrationsAssembly(
                    "Chapeau.Migrations.Postgres"));
        }
        else
        {
            options.UseSqlite("Data Source=chapeau_dev.db");
        }

        return new ChapeauDbContext(options.Options);
    }
}