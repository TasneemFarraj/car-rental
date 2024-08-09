using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class setCountryAddTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Automatic",
                table: "CarSlider");

            migrationBuilder.AddColumn<int>(
                name: "Country",
                table: "CarSlider",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Transmission",
                table: "CarSlider",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "CarSlider");

            migrationBuilder.DropColumn(
                name: "Transmission",
                table: "CarSlider");

            migrationBuilder.AddColumn<string>(
                name: "Automatic",
                table: "CarSlider",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
