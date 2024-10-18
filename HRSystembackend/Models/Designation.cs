// Models / Designation.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystemBackend.Models
{
    public class Designation
    {
        [Key]
        public int DesigID { get; set; }
        public int ComID { get; set; }
        public string DesigName { get; set; }

        [ForeignKey("ComID")]
        public Company Company { get; set; }
    }
}
