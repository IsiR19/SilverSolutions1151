using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SilverSolutions1151.Data.Entity;
using SilverSolutions1151.Models;
using SilverSolutions1151.Models.Entity;

namespace SilverSolutions1151.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
        }

        public DbSet<Catalog>? Catalog { get; set; }
        public DbSet<ProductType>? ProductType { get; set; }
        public DbSet<RawMaterial>? RawMaterials { get; set; }
        public DbSet<Packaging>? Packaging { get; set; }
        public DbSet<ProductTypeIngredients>? ProductTypeIngredients { get; set; }
        public DbSet<Inventory>? Inventory { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<ProductionReport> ProductionReport { get; set; }
        public DbSet<ManufacturingStage> ManufacturingStage { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SalesOrder> SalesOrder { get; set; }
        public DbSet<Packing> Packing { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<SilverSolutions1151.Models.Entity.InvoiceDetails> InvoiceDetails { get; set; }
        public DbSet<SilverSolutions1151.Models.Entity.Sale> Sale { get; set; }
        public DbSet<SilverSolutions1151.Models.Entity.SalesDetail> SalesDetails { get; set; }
        public DbSet<Manufacture> Manufacturing { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductStock> ProductStocks { get; set; }
    }
}