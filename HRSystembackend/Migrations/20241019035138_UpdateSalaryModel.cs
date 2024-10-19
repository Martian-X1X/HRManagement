using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRSystembackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSalaryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalaryID",
                table: "Salaries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalaryID",
                table: "Salaries",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
