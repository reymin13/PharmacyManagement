using PharmacyManagement.Application.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagement.Application.Models
{
    [Table("Sales")]
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }



        public int Quantity { get; set; }

        public decimal Discount { get; set; }

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        [InverseProperty("Sale")]
        public virtual ICollection<ConfirmedSale> ConfirmedSales { get; set; } = new List<ConfirmedSale>();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Sale()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
        }
    }
}


[Table("ConfirmedSales")]
public class ConfirmedSale
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime ConfirmedDate { get; set; }

    [ForeignKey("Sale")]
    public int SaleId { get; set; }


    [InverseProperty("ConfirmedSales")]
    public virtual Sale Sale { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }

    public virtual Product Product { get; set; }
    [Required]
    public bool Paid { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public ConfirmedSale()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
}
