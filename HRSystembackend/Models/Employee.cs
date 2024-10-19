// Models/Employee.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystemBackend.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment for EmpID
        public int EmpID { get; set; }

        [Required]
        public int ComID { get; set; }

        [Required]
        public int ShiftID { get; set; }

        [Required]
        public int DeptID { get; set; }

        [Required]
        public int DesigID { get; set; }

        [Required]
        [StringLength(50)]
        public string EmpCode { get; set; }

        [Required]
        [StringLength(100)]
        public string EmpName { get; set; }

        public string Gender { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Required]
        public decimal Gross { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Basic { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal HRent { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Medical { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Others { get; set; }

        [Required]
        public DateTime DtJoin { get; set; }

        // Navigation properties (optional if you want to include related entities)
        [ForeignKey("ComID")]
        public Company Company { get; set; }

        [ForeignKey("ShiftID")]
        public Shift Shift { get; set; }

        [ForeignKey("DeptID")]
        public Department Department { get; set; }

        [ForeignKey("DesigID")]
        public Designation Designation { get; set; }
    }
}
