using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagement.Models
{
    public enum PaymentMethod
    {
        Cash = 1,
        Card = 2
    }

    [Table("Payments")]
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public PaymentMethod Method { get; set; } = PaymentMethod.Cash; // Standardwert

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public bool InsurancePayment { get; set; } = false; // Standardwert

        // Beziehung zu "Sale"
        [ForeignKey("Sale")]
        [Required]
        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }

        // Beziehung zu "InsuranceProvider"
        [ForeignKey("InsuranceProvider")]
        public int? InsuranceProviderId { get; set; }
        public virtual InsuranceProvider? InsuranceProvider { get; set; }
    }
}
