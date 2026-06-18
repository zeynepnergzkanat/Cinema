using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddKoltukNumaralari : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KoltukNumaralari",
                schema: "app",
                table: "Sepetler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KoltukNumaralari",
                schema: "app",
                table: "Biletler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KoltukNumaralari",
                schema: "app",
                table: "Sepetler");

            migrationBuilder.DropColumn(
                name: "KoltukNumaralari",
                schema: "app",
                table: "Biletler");
        }
    }
}
