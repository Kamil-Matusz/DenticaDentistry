using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dentica_Dentistry.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Inint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DentistIndustries",
                columns: table => new
                {
                    DentistIndustryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DentistIndustries", x => x.DentistIndustryId);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<Guid>(type: "uuid", nullable: false),
                    DentistIndustryId = table.Column<int>(type: "integer", nullable: false),
                    BookerName = table.Column<string>(type: "text", nullable: true),
                    ReservationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_DentistIndustries_DentistIndustryId",
                        column: x => x.DentistIndustryId,
                        principalTable: "DentistIndustries",
                        principalColumn: "DentistIndustryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DentistIndustryId",
                table: "Reservations",
                column: "DentistIndustryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "DentistIndustries");
        }
    }
}
