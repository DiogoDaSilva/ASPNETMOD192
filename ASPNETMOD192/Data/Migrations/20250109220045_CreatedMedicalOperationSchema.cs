using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNETMOD192.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatedMedicalOperationSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalOperation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Informations = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalOperation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentMedicalOperation",
                columns: table => new
                {
                    AppointmentsID = table.Column<int>(type: "int", nullable: false),
                    MedicalOperationsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentMedicalOperation", x => new { x.AppointmentsID, x.MedicalOperationsID });
                    table.ForeignKey(
                        name: "FK_AppointmentMedicalOperation_Appointments_AppointmentsID",
                        column: x => x.AppointmentsID,
                        principalTable: "Appointments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentMedicalOperation_MedicalOperation_MedicalOperationsID",
                        column: x => x.MedicalOperationsID,
                        principalTable: "MedicalOperation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentMedicalOperation_MedicalOperationsID",
                table: "AppointmentMedicalOperation",
                column: "MedicalOperationsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentMedicalOperation");

            migrationBuilder.DropTable(
                name: "MedicalOperation");
        }
    }
}
