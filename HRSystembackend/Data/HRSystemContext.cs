// Data/HRSystemContext.cs
using Microsoft.EntityFrameworkCore;
using HRSystemBackend.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace HRSystemBackend.Data
{
    public class HRSystemContext : DbContext
    {
        public HRSystemContext(DbContextOptions<HRSystemContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<AttendanceSummary> AttendanceSummaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite keys
            modelBuilder.Entity<Salary>()
                .HasKey(s => new { s.ComID, s.EmpID, s.DtYear, s.DtMonth });

            modelBuilder.Entity<AttendanceSummary>()
                .HasKey(a => new { a.ComID, a.EmpID, a.DtYear, a.DtMonth });

            // Add any additional configurations here
        }
    }
}