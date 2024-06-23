using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class setHomeeditTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Home",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
              name: "Image",
              table: "Home",
              type: "nvarchar(max)",
              nullable: false,
              oldClrType: typeof(int),
              oldType: "int");
        }
    }
}
