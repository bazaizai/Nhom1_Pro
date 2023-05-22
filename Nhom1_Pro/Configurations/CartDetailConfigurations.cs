using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nhom1_Pro.Models;

namespace Nhom1_Pro.Configurations
{
    public class CartDetailConfiguration : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserID).HasColumnType("UNIQUEIDENTIFIER");
            builder.Property(x => x.DetailProductID).HasColumnType("UNIQUEIDENTIFIER");
            builder.Property(x => x.Soluong).HasColumnType("int");
            builder.Property(x => x.Dongia).HasColumnType("decimal");
            builder.Property(x => x.TrangThai).HasColumnType("int");
            builder.HasOne(x => x.Cart).WithMany(x => x.cartdetail).HasForeignKey(x => x.UserID);
            builder.HasOne(x => x.ProductDetail).WithMany(x => x.CartDetail).HasForeignKey(x => x.DetailProductID);
        }
    }
}
