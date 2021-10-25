using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdateCastTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Character",
                table: "Cast",
                newName: "TmdbUrl");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Cast",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Cast");

            migrationBuilder.RenameColumn(
                name: "TmdbUrl",
                table: "Cast",
                newName: "Character");
        }
    }
}
