using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using PharmacyManagement.Application.Models;

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
        public DbSet<PriceHistory> PriceHistories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // 🛒 Sale-Product (n:m über Sale_has_Product)
            modelBuilder.Entity<ConfirmedSale>()
                .HasKey(sp => new { sp.SaleId, sp.ProductId });

            modelBuilder.Entity<ConfirmedSale>()
                .HasOne(sp => sp.Sale)
                .WithMany(s => s.ConfirmedSales)
                .HasForeignKey(sp => sp.SaleId);

            modelBuilder.Entity<ConfirmedSale>()
                .HasOne(sp => sp.Product)
                .WithMany(p => p.Sales)
                .HasForeignKey(sp => sp.ProductId);

     



        }

    }
}

