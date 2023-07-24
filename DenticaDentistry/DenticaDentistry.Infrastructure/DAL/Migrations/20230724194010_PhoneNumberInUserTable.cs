using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dentica_Dentistry.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class PhoneNumberInUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "character varying(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");
        }
    }
}
