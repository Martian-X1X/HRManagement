// Models/Salary.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystemBackend.Models
{
    public class Salary
    {
        [Key]
        public int SalaryID { get; set; }
        public int ComID { get; set; }
        public int EmpID { get; set; }
        public int DtYear { get; set; }
        public int DtMonth { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Gross { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Basic { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Hrent { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Medical { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal AbsentAmount { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal PayableAmount { get; set; }
        public bool IsPaid { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal PaidAmount { get; set; }

        [ForeignKey("ComID")]
        public Company Company { get; set; }
        [ForeignKey("EmpID")]
        public Employee Employee { get; set; }
    }
}
