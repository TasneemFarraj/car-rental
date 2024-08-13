using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class AddTransmissionn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Automatic",
                table: "CarDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Automatic",
                table: "CarDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
