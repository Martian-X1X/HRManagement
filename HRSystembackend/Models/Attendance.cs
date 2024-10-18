// Models/Attendance.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystemBackend.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceID { get; set; }
        public int ComID { get; set; }
        public int EmpID { get; set; }
        public DateTime DtDate { get; set; }
        public string AttStatus { get; set; }
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }

        [ForeignKey("ComID")]
        public Company Company { get; set; }
        [ForeignKey("EmpID")]
        public Employee Employee { get; set; }
    }
}