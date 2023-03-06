using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dentica_Dentistry.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class create_new_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DentistIndustries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DentistIndustries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "DentistIndustries",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "DentistIndustries");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DentistIndustries");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "DentistIndustries");
        }
    }
}
