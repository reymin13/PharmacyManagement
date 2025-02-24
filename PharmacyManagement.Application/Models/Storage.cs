using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagement.Application.Models
{
    [Table("Storages")]
    public class Storage
    {
        [Key]
        public int StorageId { get; set; }

        public int CurrentStock { get; set; }

        public int MaxStock { get; set; }
        public Employee? Employee { get; set; }

    // 🔹 1-zu-1 Beziehung mit Product
    public virtual Product? Product { get; set; }
}
}
