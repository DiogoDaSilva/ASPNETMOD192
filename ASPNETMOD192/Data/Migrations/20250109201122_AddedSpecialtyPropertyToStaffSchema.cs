using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNETMOD192.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSpecialtyPropertyToStaffSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Specialty",
                table: "Staff",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Staff");
        }
    }
}
