using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagement.Models
{
    [Table("Prescriptions")]
    public class Prescription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [MaxLength(255)]
        public string? DoctorName { get; set; } // Optional

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public bool IsUsed { get; set; } = false; // Standardmäßig nicht verwendet
    }
}
