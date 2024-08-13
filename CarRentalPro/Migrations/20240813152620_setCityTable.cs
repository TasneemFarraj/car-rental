using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class setCityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.AddColumn<int>(
                name: "Country",
                table: "CarDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
