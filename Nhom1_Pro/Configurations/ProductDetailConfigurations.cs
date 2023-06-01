using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nhom1_Pro.Models;

namespace Nhom1_Pro.Configurations
{
    public class ProductDetailConfiguration : IEntityTypeConfiguration<ProductDetail>
    {
        public void Configure(EntityTypeBuilder<ProductDetail> builder)
        {
        builder.HasKey(x => x.Id);
            builder.Property(x => x.BaoHanh).HasColumnType("nvarchar(250)").IsRequired(false);
            builder.Property(x => x.MoTa).HasColumnType("nvarchar(1000)").IsRequired(false);

            builder.HasOne(x => x.Product).WithMany(y => y.ProductDetail).
            HasForeignKey(c => c.IdProduct);

            builder.HasOne(x => x.Color).WithMany(y => y.ProductDetail).
            HasForeignKey(c => c.IdColor);

            builder.HasOne(x => x.Size).WithMany(y => y.ProductDetail).
            HasForeignKey(c => c.IdSize);


            builder.HasOne(x => x.TypeProduct).WithMany(y => y.ProductDetails).
            HasForeignKey(c => c.IdTypeProduct);


            builder.HasOne(x => x.Material).WithMany(y => y.ProductDetail).
            HasForeignKey(c => c.IdMaterial);
            


        }
    }
}
