using Microsoft.EntityFrameworkCore.Migrations;

namespace DbService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Sample",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("MySQL:AutoIncrement", true),
                    Value = table.Column<long>()
                },
                constraints: table => { table.PrimaryKey("PK_Sample", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Sample");
        }
    }
}