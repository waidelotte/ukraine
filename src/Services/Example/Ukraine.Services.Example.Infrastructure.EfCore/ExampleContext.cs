using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ukraine.Infrastructure.EfCore;
using Ukraine.Infrastructure.EfCore.Contexts;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Infrastructure.EfCore;

public class ExampleContext : AppDbContextBase
{
    private const string SCHEMA = "example_schema";
        
    public DbSet<ExampleEntity> ExampleEntities => Set<ExampleEntity>();

    public ExampleContext(DbContextOptions options) : base(options) { }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SCHEMA);
        modelBuilder.HasPostgresExtension(Constants.UUID_GENERATOR);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}