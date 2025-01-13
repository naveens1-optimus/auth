using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Authentication.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Naveen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Roles",
                table: "Users",
                newName: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "Roles");
        }
    }
}
