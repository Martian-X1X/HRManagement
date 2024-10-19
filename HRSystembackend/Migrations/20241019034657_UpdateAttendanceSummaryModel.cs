using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HRSystembackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAttendanceSummaryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendances",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_ComID",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "SummaryID",
                table: "AttendanceSummaries");

            migrationBuilder.DropColumn(
                name: "AttendanceID",
                table: "Attendances");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendances",
                table: "Attendances",
                columns: new[] { "ComID", "EmpID", "DtDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendances",
                table: "Attendances");

            migrationBuilder.AddColumn<int>(
                name: "SummaryID",
                table: "AttendanceSummaries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttendanceID",
                table: "Attendances",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendances",
                table: "Attendances",
                column: "AttendanceID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ComID",
                table: "Attendances",
                column: "ComID");
        }
    }
}
