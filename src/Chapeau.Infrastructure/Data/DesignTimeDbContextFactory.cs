using  Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Chapeau.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ChapeauDbContext>
{
    public ChapeauDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<ChapeauDbContext>();
        options.UseSqlite("Data Source=chapeau_dev.db");
        return new ChapeauDbContext(options.Options);
    }
}