using DbService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DbService.Migrations
{
    [DbContext(typeof(SampleDbContext))]
    internal class SampleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("DbService.Entities.Sample", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<long>("Value");

                b.HasKey("Id");

                b.ToTable("Sample");
            });
#pragma warning restore 612, 618
        }
    }
}