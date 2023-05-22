using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nhom1_Pro.Models;

namespace Nhom1_Pro.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.UserID);
            builder.Property(c => c.Mota).HasColumnType("nvarchar(MAX)").IsRequired(false);
            builder.Property(c => c.TrangThai).HasColumnType("int").IsRequired();
        }
    }
}
