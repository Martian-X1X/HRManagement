using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystemBackend.Models
{
    public class Employee
    {
        [Key]
        public int EmpID { get; set; }
        public int ComID { get; set; }
        public int ShiftID { get; set; }
        public int DeptID { get; set; }
        public int DesigID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string Gender { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Gross { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Basic { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal HRent { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Medical { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Others { get; set; }
        public DateTime DtJoin { get; set; }

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