﻿// <auto-generated />
using System;
using HRSystemBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HRSystembackend.Migrations
{
    [DbContext(typeof(HRSystemContext))]
    [Migration("20241019053142_AddShiftsTable")]
    partial class AddShiftsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HRSystemBackend.Models.Attendance", b =>
                {
                    b.Property<int>("ComID")
                        .HasColumnType("integer");

                    b.Property<int>("EmpID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DtDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("AttStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeSpan>("InTime")
                        .HasColumnType("interval");

                    b.Property<TimeSpan>("OutTime")
                        .HasColumnType("interval");

                    b.HasKey("ComID", "EmpID", "DtDate");

                    b.HasIndex("EmpID");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("HRSystemBackend.Models.AttendanceSummary", b =>
                {
                    b.Property<int>("ComID")
                        .HasColumnType("integer");

                    b.Property<int>("EmpID")
                        .HasColumnType("integer");

                    b.Property<int>("DtYear")
                        .HasColumnType("integer");

                    b.Property<int>("DtMonth")
                        .HasColumnType("integer");

                    b.Property<int>("Absent")
                        .HasColumnType("integer");

                    b.HasKey("ComID", "EmpID", "DtYear", "DtMonth");

                    b.HasIndex("EmpID");

                    b.ToTable("AttendanceSummaries");
                });

            modelBuilder.Entity("HRSystemBackend.Models.Company", b =>
                {
                    b.Property<int>("ComID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ComID"));

                    b.Property<decimal>("Basic")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("ComName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Hrent")
                        .HasColumnType("decimal(10,2)");

                    b.Property<bool>("IsInactive")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Medical")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("ComID");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("HRSystemBackend.Models.Department", b =>
                {
                    b.Property<int>("DeptID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DeptID"));

                    b.Property<int>("ComID")
                        .HasColumnType("integer");

                    b.Property<string>("DeptName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DeptID");

                    b.HasIndex("ComID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("HRSystemBackend.Models.Designation", b =>
                {
                    b.Property<int>("DesigID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DesigID"));

                    b.Property<int>("ComID")
                        .HasColumnType("integer");

                    b.Property<string>("DesigName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DesigID");

                    b.HasIndex("ComID");

                    b.ToTable("Designations");
                });

            modelBuilder.Entity("HRSystemBackend.Models.Employee", b =>
                {
                    b.Property<int>("EmpID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EmpID"));

                    b.Property<decimal>("Basic")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("ComID")
                        .HasColumnType("integer");

                    b.Property<int>("DeptID")
                        .HasColumnType("integer");

                    b.Property<int>("DesigID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DtJoin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EmpCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EmpName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Gross")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("HRent")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("Medical")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("Others")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("ShiftID")
                        .HasColumnType("integer");

                    b.HasKey("EmpID");

                    b.HasIndex("ComID");

                    b.HasIndex("DeptID");

                    b.HasIndex("DesigID");

                    b.HasIndex("ShiftID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HRSystemBackend.Models.Salary", b =>
                {
                    b.Property<int>("ComID")
                        .HasColumnType("integer");

                    b.Property<int>("EmpID")
                        .HasColumnType("integer");

                    b.Property<int>("DtYear")
                        .HasColumnType("integer");

                    b.Property<int>("DtMonth")
                        .HasColumnType("integer");

                    b.Property<decimal>("AbsentAmount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("Basic")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("Gross")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("Hrent")
                        .HasColumnType("decimal(10,2)");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Medical")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("PaidAmount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("PayableAmount")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("ComID", "EmpID", "DtYear", "DtMonth");

                    b.HasIndex("EmpID");

                    b.ToTable("Salaries");
                });

            modelBuilder.Entity("HRSystemBackend.Models.Shift", b =>
                {
                    b.Property<int>("ShiftID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ShiftID"));

                    b.Property<int>("ComID")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("ShiftIn")
                        .HasColumnType("interval");

                    b.Property<TimeSpan>("ShiftLate")
                        .HasColumnType("interval");

                    b.Property<string>("ShiftName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeSpan>("ShiftOut")
                        .HasColumnType("interval");

                    b.HasKey("ShiftID");

                    b.HasIndex("ComID");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("HRSystemBackend.Models.Attendance", b =>
                {
                    b.HasOne("HRSystemBackend.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("ComID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRSystemBackend.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmpID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HRSystemBackend.Models.AttendanceSummary", b =>
                {
                    b.HasOne("HRSystemBackend.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("ComID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRSystemBackend.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmpID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HRSystemBackend.Models.Department", b =>
                {
                    b.HasOne("HRSystemBackend.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("ComID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("HRSystemBackend.Models.Designation", b =>
                {
                    b.HasOne("HRSystemBackend.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("ComID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("HRSystemBackend.Models.Employee", b =>
                {
                    b.HasOne("HRSystemBackend.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("ComID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRSystemBackend.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DeptID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRSystemBackend.Models.Designation", "Designation")
                        .WithMany()
                        .HasForeignKey("DesigID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRSystemBackend.Models.Shift", "Shift")
                        .WithMany()
                        .HasForeignKey("ShiftID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Department");

                    b.Navigation("Designation");

                    b.Navigation("Shift");
                });

            modelBuilder.Entity("HRSystemBackend.Models.Salary", b =>
                {
                    b.HasOne("HRSystemBackend.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("ComID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRSystemBackend.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmpID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HRSystemBackend.Models.Shift", b =>
                {
                    b.HasOne("HRSystemBackend.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("ComID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("HRSystemBackend.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
