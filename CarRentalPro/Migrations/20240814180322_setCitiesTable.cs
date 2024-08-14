using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class setCitiesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarDetails_City_CityId",
                table: "CarDetails");

            migrationBuilder.DropIndex(
                name: "IX_CarDetails_CityId",
                table: "CarDetails");
        }
    }
}
