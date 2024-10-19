using HRSystemBackend.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Shift
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ShiftID { get; set; }

    public int ComID { get; set; } // Foreign key from Company table

    public string ShiftName { get; set; }

    public TimeSpan ShiftIn { get; set; }

    public TimeSpan ShiftOut { get; set; }

    public TimeSpan ShiftLate { get; set; }

    [ForeignKey("ComID")]
    public Company Company { get; set; } // Virtual for lazy loading
}
