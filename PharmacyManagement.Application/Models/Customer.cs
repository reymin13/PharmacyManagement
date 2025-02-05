using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagement.Application.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        public string InsuranceNumber { get; set; }

        [Required, MaxLength(20)]
        public string Contact { get; set; }

        [Range(0, double.MaxValue)]
        public decimal LoyaltyPoints { get; set; }

        [ForeignKey("InsuranceProvider")]
        public int? InsuranceProviderId { get; set; }
        public virtual InsuranceProvider InsuranceProvider { get; set; }
    }



    [Table("InsuranceProviders")]
    public class InsuranceProvider
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        public string ContactInfo { get; set; }
    }
}

