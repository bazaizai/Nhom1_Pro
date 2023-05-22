using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nhom1_Pro.Models;

namespace Nhom1_Pro.Configurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Material");
            builder.HasKey(x => x.Id);
            builder.Property(c => c.Ma).HasColumnType("nvarchar(1000)").IsRequired(true);
            builder.Property(c => c.Ten).HasColumnType("nvarchar(1000)").IsRequired(true);
            builder.Property(c => c.TrangThai).HasColumnType("int");
        }
    }
}
