using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalPro.Migrations
{
    /// <inheritdoc />
    public partial class setUpdForTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PasswordResets",
                table: "PasswordResets");

            migrationBuilder.RenameTable(
                name: "PasswordResets",
                newName: "PasswordReset");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PasswordReset",
                table: "PasswordReset",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PasswordReset",
                table: "PasswordReset");

            migrationBuilder.RenameTable(
                name: "PasswordReset",
                newName: "PasswordResets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PasswordResets",
                table: "PasswordResets",
                column: "Id");
        }
    }
}
