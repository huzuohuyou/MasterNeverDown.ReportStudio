using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityToolkit.ReportEditor.Model.Migrations
{
    public partial class InitialCreate6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    InputType = table.Column<int>(type: "INTEGER", nullable: false),
                    DataSourceType = table.Column<int>(type: "INTEGER", nullable: false),
                    Script = table.Column<string>(type: "TEXT", nullable: true),
                    CreateDate = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parameters");
        }
    }
}
