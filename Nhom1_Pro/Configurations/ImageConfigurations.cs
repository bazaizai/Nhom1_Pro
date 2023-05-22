using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nhom1_Pro.Models;

namespace Nhom1_Pro.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            //     public Guid Id { get; set; }
            //public Guid? IdProductDetail { get; set; }
            //public string? TenAnh { get; set; }
            //public byte[]? DuongDan { get; set; }
            //public int? TrangThai { get; set; }

            //public virtual ProductDetail ProductDetail { get; set; }

            builder.HasKey(x => x.Id);
            builder.Property(x => x.TenAnh).HasColumnType("nvarchar(250)");

            builder.HasOne(x => x.ProductDetail).WithMany(y => y.Image).
            HasForeignKey(c => c.IdProductDetail);
    }
    }
}
