using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nhom1_Pro.Models;

namespace Nhom1_Pro.Configurations
{
    public class BillDetailConfiguration : IEntityTypeConfiguration<BillDetail>
    {
        public void Configure(EntityTypeBuilder<BillDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IdBill).HasColumnType("UNIQUEIDENTIFIER");
            builder.Property(x => x.IdProductDetail).HasColumnType("UNIQUEIDENTIFIER");
            builder.Property(x => x.SoLuong).HasColumnType("int");
            builder.Property(x => x.DonGia).HasColumnType("decimal");
            builder.Property(x => x.TrangThai).HasColumnType("int");
            builder.HasOne(x => x.Bill).WithMany(x => x.BillDetails).HasForeignKey(x => x.IdBill);
            builder.HasOne(x => x.ProductDetail).WithMany(x => x.BillDetail).HasForeignKey(x => x.IdProductDetail);
        }
    }
}
