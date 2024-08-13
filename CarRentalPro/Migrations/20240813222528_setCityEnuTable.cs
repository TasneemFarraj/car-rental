using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class setCityEnuTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarDetails_City_CityId",
                table: "CarDetails");

            migrationBuilder.DropIndex(
                name: "IX_CarDetails_CityId",
                table: "CarDetails");

            migrationBuilder.AddColumn<int>(
                name: "CityName",
                table: "CarDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityName",
                table: "CarDetails");

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
        }
    }
}
