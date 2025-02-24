using PharmacyManagement.Application.Models;
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

        [ForeignKey("StorageId")]
        public virtual Storage Storage { get; set; }

        [ForeignKey("CategoryNavigation")]
        public int CategoryId { get; set; }
        public virtual Category CategoryNavigation { get; set; }


        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<ConfirmedSale> Sales { get; set; } = new List<ConfirmedSale>();//TODO: Check if this is correct

        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

        public ICollection<PriceHistory> priceHistories { get; set; } = new List<PriceHistory>();


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Product()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
        }
    }

    public class PriceHistory
    {
        [Key]
        public int PriceHistoryId { get; set; }

        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }



    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string? Name { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();


    }
}

