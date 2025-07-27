using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library.DataModel.Migrations
{
    /// <inheritdoc />
    public partial class TwoFieldInIssuedBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FineType",
                table: "IssuedBooks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinePaid",
                table: "IssuedBooks",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FineType",
                table: "IssuedBooks");

            migrationBuilder.DropColumn(
                name: "IsFinePaid",
                table: "IssuedBooks");
        }
    }
}
