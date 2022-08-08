using Microsoft.EntityFrameworkCore;
using Ukraine.Infrastructure.EfCore;
using Ukraine.Infrastructure.EfCore.Contexts;

namespace FirstTestApiService
{
    public class Context : AppDbContextBase
    {
        private const string SCHEMA = "test_first";

        public Context(DbContextOptions options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SCHEMA);
            modelBuilder.HasPostgresExtension(Constants.UUID_GENERATOR);
        }
    }
}
