using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HRSystembackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    ComID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComName = table.Column<string>(type: "text", nullable: false),
                    Basic = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    Hrent = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    Medical = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    IsInactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ComID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DeptID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComID = table.Column<int>(type: "integer", nullable: false),
                    DeptName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DeptID);
                    table.ForeignKey(
                        name: "FK_Departments_Companies_ComID",
                        column: x => x.ComID,
                        principalTable: "Companies",
                        principalColumn: "ComID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    DesigID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComID = table.Column<int>(type: "integer", nullable: false),
                    DesigName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.DesigID);
                    table.ForeignKey(
                        name: "FK_Designations_Companies_ComID",
                        column: x => x.ComID,
                        principalTable: "Companies",
                        principalColumn: "ComID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    ShiftID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComID = table.Column<int>(type: "integer", nullable: false),
                    ShiftName = table.Column<string>(type: "text", nullable: false),
                    ShiftIn = table.Column<TimeSpan>(type: "interval", nullable: false),
                    ShiftOut = table.Column<TimeSpan>(type: "interval", nullable: false),
                    ShiftLate = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.ShiftID);
                    table.ForeignKey(
                        name: "FK_Shifts_Companies_ComID",
                        column: x => x.ComID,
                        principalTable: "Companies",
                        principalColumn: "ComID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmpID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComID = table.Column<int>(type: "integer", nullable: false),
                    ShiftID = table.Column<int>(type: "integer", nullable: false),
                    DeptID = table.Column<int>(type: "integer", nullable: false),
                    DesigID = table.Column<int>(type: "integer", nullable: false),
                    EmpCode = table.Column<string>(type: "text", nullable: false),
                    EmpName = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Gross = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Basic = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    HRent = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Medical = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Others = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    DtJoin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmpID);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_ComID",
                        column: x => x.ComID,
                        principalTable: "Companies",
                        principalColumn: "ComID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DeptID",
                        column: x => x.DeptID,
                        principalTable: "Departments",
                        principalColumn: "DeptID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Designations_DesigID",
                        column: x => x.DesigID,
                        principalTable: "Designations",
                        principalColumn: "DesigID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Shifts_ShiftID",
                        column: x => x.ShiftID,
                        principalTable: "Shifts",
                        principalColumn: "ShiftID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    AttendanceID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComID = table.Column<int>(type: "integer", nullable: false),
                    EmpID = table.Column<int>(type: "integer", nullable: false),
                    DtDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AttStatus = table.Column<string>(type: "text", nullable: false),
                    InTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    OutTime = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.AttendanceID);
                    table.ForeignKey(
                        name: "FK_Attendances_Companies_ComID",
                        column: x => x.ComID,
                        principalTable: "Companies",
                        principalColumn: "ComID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendances_Employees_EmpID",
                        column: x => x.EmpID,
                        principalTable: "Employees",
                        principalColumn: "EmpID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceSummaries",
                columns: table => new
                {
                    ComID = table.Column<int>(type: "integer", nullable: false),
                    EmpID = table.Column<int>(type: "integer", nullable: false),
                    DtYear = table.Column<int>(type: "integer", nullable: false),
                    DtMonth = table.Column<int>(type: "integer", nullable: false),
                    SummaryID = table.Column<int>(type: "integer", nullable: false),
                    Absent = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceSummaries", x => new { x.ComID, x.EmpID, x.DtYear, x.DtMonth });
                    table.ForeignKey(
                        name: "FK_AttendanceSummaries_Companies_ComID",
                        column: x => x.ComID,
                        principalTable: "Companies",
                        principalColumn: "ComID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceSummaries_Employees_EmpID",
                        column: x => x.EmpID,
                        principalTable: "Employees",
                        principalColumn: "EmpID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    ComID = table.Column<int>(type: "integer", nullable: false),
                    EmpID = table.Column<int>(type: "integer", nullable: false),
                    DtYear = table.Column<int>(type: "integer", nullable: false),
                    DtMonth = table.Column<int>(type: "integer", nullable: false),
                    SalaryID = table.Column<int>(type: "integer", nullable: false),
                    Gross = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Basic = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Hrent = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Medical = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    AbsentAmount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    PayableAmount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => new { x.ComID, x.EmpID, x.DtYear, x.DtMonth });
                    table.ForeignKey(
                        name: "FK_Salaries_Companies_ComID",
                        column: x => x.ComID,
                        principalTable: "Companies",
                        principalColumn: "ComID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Salaries_Employees_EmpID",
                        column: x => x.EmpID,
                        principalTable: "Employees",
                        principalColumn: "EmpID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ComID",
                table: "Attendances",
                column: "ComID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmpID",
                table: "Attendances",
                column: "EmpID");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSummaries_EmpID",
                table: "AttendanceSummaries",
                column: "EmpID");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ComID",
                table: "Departments",
                column: "ComID");

            migrationBuilder.CreateIndex(
                name: "IX_Designations_ComID",
                table: "Designations",
                column: "ComID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ComID",
                table: "Employees",
                column: "ComID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DeptID",
                table: "Employees",
                column: "DeptID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesigID",
                table: "Employees",
                column: "DesigID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ShiftID",
                table: "Employees",
                column: "ShiftID");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_EmpID",
                table: "Salaries",
                column: "EmpID");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ComID",
                table: "Shifts",
                column: "ComID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "AttendanceSummaries");

            migrationBuilder.DropTable(
                name: "Salaries");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
