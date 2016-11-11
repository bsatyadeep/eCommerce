using System.Data.Entity;
using eCommerce.Model;
using eCommerce.DataAccess.Migrations;

namespace eCommerce.DataAccess.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<VoucherType> VoucherTypes { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<BasketVoucher> BasketVouchers { get; set; }
        public DataContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, DataContextMigrationConfiguration>());
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
