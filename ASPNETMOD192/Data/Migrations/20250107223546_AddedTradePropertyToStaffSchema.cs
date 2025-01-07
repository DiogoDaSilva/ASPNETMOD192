using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNETMOD192.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedTradePropertyToStaffSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Trade",
                table: "Staff",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Trade",
                table: "Staff");
        }
    }
}
