using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrupoArchicentroWebAppTest.Migrations
{
    public partial class createdatadni : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DNI",
                table: "Empleado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DNI",
                table: "Empleado");
        }
    }
}
