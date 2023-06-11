using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Nhom1_Pro.Models
{
    public class DBContextModel : DbContext
    {
        public DBContextModel() { }
        public DBContextModel(DbContextOptions options) : base(options) { }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<SaleDetail> DetailSales { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<TypeProduct> TypeProducts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=Mi\SQLEXPRESS;Initial Catalog=ProductNewShop;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.
                ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
