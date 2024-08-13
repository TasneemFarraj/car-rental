using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class setResDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfDays",
                table: "CarReservation");

            migrationBuilder.RenameColumn(
                name: "PickupDate",
                table: "CarReservation",
                newName: "PickUpDate");

            migrationBuilder.RenameColumn(
                name: "DropoffDate",
                table: "CarReservation",
                newName: "DropOfDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PickUpDate",
                table: "CarReservation",
                newName: "PickupDate");

            migrationBuilder.RenameColumn(
                name: "DropOfDate",
                table: "CarReservation",
                newName: "DropoffDate");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDays",
                table: "CarReservation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
