using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystemBackend.Models
{
    public class Department
    {
        [Key]
        public int DeptID { get; set; }
        public int ComID { get; set; }
        public string DeptName { get; set; }

        [ForeignKey("ComID")]
        public Company Company { get; set; }

        // Change this to a collection of Employee entities
        public ICollection<Employee> Employees { get; set; } // Update here
    }
}
