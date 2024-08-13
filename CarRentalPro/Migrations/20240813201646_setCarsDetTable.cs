using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class setCarsDetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_CarReservation_ReservationId",
                table: "PaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDetails_ReservationId",
                table: "PaymentDetails");

            migrationBuilder.AddColumn<int>(
                name: "CarReservationId",
                table: "PaymentDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "CarDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_CarReservationId",
                table: "PaymentDetails",
                column: "CarReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_CarDetails_CityId",
                table: "CarDetails",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarDetails_City_CityId",
                table: "CarDetails",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_CarReservation_CarReservationId",
                table: "PaymentDetails",
                column: "CarReservationId",
                principalTable: "CarReservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarDetails_City_CityId",
                table: "CarDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_CarReservation_CarReservationId",
                table: "PaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDetails_CarReservationId",
                table: "PaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_CarDetails_CityId",
                table: "CarDetails");

            migrationBuilder.DropColumn(
                name: "CarReservationId",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "CarDetails");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_ReservationId",
                table: "PaymentDetails",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_CarReservation_ReservationId",
                table: "PaymentDetails",
                column: "ReservationId",
                principalTable: "CarReservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
