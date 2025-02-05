using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Models;  

namespace PharmacyManagement.Application.data  
{
    public class PharmacyDbContext : DbContext
    {
        public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ConfirmedSale> ConfirmedSales { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<InsuranceProvider> InsuranceProviders { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Payment> Payments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    } 
}  
