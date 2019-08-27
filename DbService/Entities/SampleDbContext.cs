using Microsoft.EntityFrameworkCore;

namespace DbService.Entities
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> contextOptions) : base(contextOptions)
        {
        }

        public DbSet<Sample> Sample { get; set; }
    }
}