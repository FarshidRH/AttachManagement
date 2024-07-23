using Microsoft.EntityFrameworkCore.Design;

namespace AttachManagement.Infrastructure.Data;

internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.UseSqlServer();
        return new AppDbContext(builder.Options);
    }
}
