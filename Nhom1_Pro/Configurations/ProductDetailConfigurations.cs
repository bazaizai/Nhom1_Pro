using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nhom1_Pro.Models;

namespace Nhom1_Pro.Configurations
{
    public class ProductDetailConfiguration : IEntityTypeConfiguration<ProductDetail>
    {
        public void Configure(EntityTypeBuilder<ProductDetail> builder)
        {
        //public Guid Id { get; set; }
        //public Guid? IdProduct { get; set; }
        //public Guid? IdColor { get; set; }
        //public Guid? IdSize { get; set; }
        //public Guid? IdTypeProduct { get; set; }
        //public Guid? IdMaterial { get; set; }
        //public string? BaoHanh { get; set; }
        //public string? MoTa { get; set; }
        //public int? SoLuongTon { get; set; }
        //public decimal? GiaNhap { get; set; }
        //public decimal? GiaBan { get; set; }
        //public int? TrangThai { get; set; }
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
