using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class setCCarReservationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "CarReservation",
                newName: "PickupDate");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "CarReservation",
                newName: "DropoffDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PickupDate",
                table: "CarReservation",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "DropoffDate",
                table: "CarReservation",
                newName: "EndDate");
        }
    }
}
