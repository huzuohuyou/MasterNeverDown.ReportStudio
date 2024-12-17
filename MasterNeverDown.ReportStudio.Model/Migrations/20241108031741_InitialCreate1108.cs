using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityToolkit.ReportEditor.Model.Migrations
{
    public partial class InitialCreate1108 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DataSource",
                table: "Templates",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Connection",
                table: "Parameters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Connection",
                table: "Parameters");

            migrationBuilder.AlterColumn<string>(
                name: "DataSource",
                table: "Templates",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
