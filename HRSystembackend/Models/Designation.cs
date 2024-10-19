using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystemBackend.Models
{
    public class Designation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment for DesigID
        public int DesigID { get; set; }

        public int ComID { get; set; } // Foreign key from the Company table

        public string DesigName { get; set; }

        [ForeignKey("ComID")]
        public Company Company { get; set; }
    }
}
