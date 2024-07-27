namespace AttachManagement.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            AttachManagement.Infrastructure.AssemblyReference.Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
