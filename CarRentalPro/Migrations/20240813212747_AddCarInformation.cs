using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class AddCarInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manual",
                table: "CarDetails");

            migrationBuilder.AddColumn<int>(
                name: "CarInformationId",
                table: "CarReservation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Transmission",
                table: "CarDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CarInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stars = table.Column<float>(type: "real", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Door = table.Column<int>(type: "int", nullable: false),
                    Seat = table.Column<int>(type: "int", nullable: false),
                    Petrol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    transmission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuitCase = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarInformation_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarReservation_CarInformationId",
                table: "CarReservation",
                column: "CarInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_CarInformation_CityId",
                table: "CarInformation",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarReservation_CarInformation_CarInformationId",
                table: "CarReservation",
                column: "CarInformationId",
                principalTable: "CarInformation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarReservation_CarInformation_CarInformationId",
                table: "CarReservation");

            migrationBuilder.DropTable(
                name: "CarInformation");

            migrationBuilder.DropIndex(
                name: "IX_CarReservation_CarInformationId",
                table: "CarReservation");

            migrationBuilder.DropColumn(
                name: "CarInformationId",
                table: "CarReservation");

            migrationBuilder.DropColumn(
                name: "Transmission",
                table: "CarDetails");

          
        }
    }
}
