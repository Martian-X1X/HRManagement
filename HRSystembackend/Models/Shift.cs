// Models/Shift.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystemBackend.Models
{
    public class Shift
    {
        [Key]
        public int ShiftID { get; set; }
        public int ComID { get; set; }
        public string ShiftName { get; set; }
        public TimeSpan ShiftIn { get; set; }
        public TimeSpan ShiftOut { get; set; }
        public TimeSpan ShiftLate { get; set; }

        [ForeignKey("ComID")]
        public Company Company { get; set; }
    }
}