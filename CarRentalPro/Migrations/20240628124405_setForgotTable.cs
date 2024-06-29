using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class setForgotTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetPin",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPin",
                table: "User");
        }
    }
}
