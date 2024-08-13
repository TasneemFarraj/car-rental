using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class setCarsDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "CarDetails",
                newName: "PricePerDay");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CarDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "CarDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CarDetails");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "CarDetails");

            migrationBuilder.RenameColumn(
                name: "PricePerDay",
                table: "CarDetails",
                newName: "Price");
        }
    }
}
