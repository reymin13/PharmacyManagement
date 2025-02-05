﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagement.Application.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required, MaxLength(45)]
        public string FirstName { get; set; }

        [Required, MaxLength(45)]
        public string LastName { get; set; }

        [Required, MaxLength(45)]
        public string Username { get; set; }

        [Required, MaxLength(55)]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } // ENUM-Wert (Admin, Pharmacist, etc.)

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("Storage")]
        public int? StorageId { get; set; }
        public virtual Storage Storage { get; set; }

        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
