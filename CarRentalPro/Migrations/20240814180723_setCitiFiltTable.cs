using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class setCitiFiltTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CarFilter_CityId",
                table: "CarFilter",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarFilter_City_CityId",
                table: "CarFilter",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarFilter_City_CityId",
                table: "CarFilter");

            migrationBuilder.DropIndex(
                name: "IX_CarFilter_CityId",
                table: "CarFilter");
        }
    }
}
