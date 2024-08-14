using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class setOffDroTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "PaymentDetails");

            migrationBuilder.RenameColumn(
                name: "DropOfDate",
                table: "CarReservation",
                newName: "DropOffDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DropOffDate",
                table: "CarReservation",
                newName: "DropOfDate");

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "PaymentDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
