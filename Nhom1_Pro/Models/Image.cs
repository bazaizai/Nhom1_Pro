using System;
using System.Collections.Generic;

namespace Nhom1_Pro.Models
{
    public partial class Image
    {
        public Guid Id { get; set; }
        public Guid? IdProductDetail { get; set; }
        public string? TenAnh { get; set; }
        public byte[]? DuongDan { get; set; }
        public int? TrangThai { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }
    }
}
