
// Models/AttendanceSummary.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystemBackend.Models
{
    public class AttendanceSummary
    {
        [Key]
        public int SummaryID { get; set; }
        public int ComID { get; set; }
        public int EmpID { get; set; }
        public int DtYear { get; set; }
        public int DtMonth { get; set; }
        public int Absent { get; set; }

        [ForeignKey("ComID")]
        public Company Company { get; set; }
        [ForeignKey("EmpID")]
        public Employee Employee { get; set; }
    }
}