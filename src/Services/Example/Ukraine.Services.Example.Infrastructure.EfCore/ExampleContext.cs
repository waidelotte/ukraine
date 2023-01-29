using Microsoft.EntityFrameworkCore;
using Ukraine.Infrastructure.EfCore;
using Ukraine.Infrastructure.EfCore.Contexts;

namespace Ukraine.Services.Example.Infrastructure.EfCore
{
    public class ExampleContext : AppDbContextBase
    {
        private const string SCHEMA = "example_schema";

        public ExampleContext(DbContextOptions options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SCHEMA);
            modelBuilder.HasPostgresExtension(Constants.UUID_GENERATOR);
        }
    }
}
