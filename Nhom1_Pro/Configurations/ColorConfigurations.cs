using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nhom1_Pro.Models;

namespace Nhom1_Pro.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable("Color");
            builder.HasKey(x => x.Id);
            builder.Property(c => c.Ma).HasColumnType("nvarchar(1000)").IsRequired(true);
            builder.Property(c => c.Ten).HasColumnType("nvarchar(1000)").IsRequired(true);
            builder.Property(c => c.TrangThai).HasColumnType("int");
        }
    }
}
