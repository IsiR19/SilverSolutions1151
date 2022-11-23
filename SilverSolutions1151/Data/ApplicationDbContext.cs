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
    }
}