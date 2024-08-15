using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class setEmeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PaymentDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "PaymentDetails");
        }
    }
}
