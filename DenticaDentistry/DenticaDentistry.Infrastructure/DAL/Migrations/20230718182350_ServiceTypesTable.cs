using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dentica_Dentistry.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ServiceTypesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceTypeId",
                table: "DentistIndustries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    ServiceTypeId = table.Column<int>(type: "integer", nullable: false),
                    ServiceTypeName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.ServiceTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DentistIndustries_ServiceTypeId",
                table: "DentistIndustries",
                column: "ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DentistIndustries_ServiceTypes_ServiceTypeId",
                table: "DentistIndustries",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "ServiceTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DentistIndustries_ServiceTypes_ServiceTypeId",
                table: "DentistIndustries");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropIndex(
                name: "IX_DentistIndustries_ServiceTypeId",
                table: "DentistIndustries");

            migrationBuilder.DropColumn(
                name: "ServiceTypeId",
                table: "DentistIndustries");
        }
    }
}
