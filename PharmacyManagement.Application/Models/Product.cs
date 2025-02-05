using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagement.Application.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }

        public string Description { get; set; }

        [MaxLength(100)]
        public string BatchNumber { get; set; }

        public DateTime ExpireDate { get; set; }

        [Required, MaxLength(255)]
        public string Manufacturer { get; set; }

        public int StorageLocation { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category CategoryNavigation { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<ConfirmedSale> Sales { get; set; } = new List<ConfirmedSale>();//TODO: Check if this is correct
    }
}


public class Category
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string Name { get; set; }
}

