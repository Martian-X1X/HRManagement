using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystemBackend.Models
{
    public class Company
    {
        [Key]
        public int ComID { get; set; }
        public string ComName { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Basic { get; set; } // 50% of Gross
        [Column(TypeName = "decimal(10,2)")]
        public decimal Hrent { get; set; } // 30% of Gross
        [Column(TypeName = "decimal(10,2)")]
        public decimal Medical { get; set; } // 15% of Gross
        public bool IsInactive { get; set; }
    }
}
