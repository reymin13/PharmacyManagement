using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagement.Application.Models
{
    [Table("Storages")]
    public class Storage
    {
        [Key]
        public int Id { get; set; }

        [Required]  // Verhindert NULL-Werte
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } // Kein "?" -> Pflichtfeld

        [Required]
        public int CurrentStock { get; set; }

        [Required]
        public int MaxStock { get; set; }
    }
}
