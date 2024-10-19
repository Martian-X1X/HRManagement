using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystemBackend.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment for DeptID
        public int DeptID { get; set; }

        public int ComID { get; set; } // Foreign key from Company table

        public string DeptName { get; set; }

        [ForeignKey("ComID")]
        public Company Company { get; set; }

        // A collection of employees in the department
        public ICollection<Employee> Employees { get; set; } = new List<Employee>(); // Initialize the collection
    }
}
