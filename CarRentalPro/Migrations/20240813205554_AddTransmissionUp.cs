using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class AddTransmissionUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Transmission",
                table: "CarDetails");

            migrationBuilder.AddColumn<string>(
                name: "Automaic",
                table: "CarDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manual",
                table: "CarDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Automaic",
                table: "CarDetails");

            migrationBuilder.DropColumn(
                name: "Manual",
                table: "CarDetails");

            migrationBuilder.AddColumn<int>(
                name: "Transmission",
                table: "CarDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
