using Microsoft.EntityFrameworkCore;

namespace DbService.Entities
{
    public class SampleDbContext : DbContext
    {
        public DbSet<Sample> Sample { get; set; }

        public SampleDbContext(DbContextOptions<SampleDbContext> contextOptions) : base(contextOptions){ }
    }
}