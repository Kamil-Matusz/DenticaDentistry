using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dentica_Dentistry.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DentistIdInReservationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DentistId",
                table: "Reservations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DentistId",
                table: "Reservations",
                column: "DentistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Dentists_DentistId",
                table: "Reservations",
                column: "DentistId",
                principalTable: "Dentists",
                principalColumn: "DentistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Dentists_DentistId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_DentistId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "DentistId",
                table: "Reservations");
        }
    }
}
