using System;
using System.Collections.Generic;

namespace Nhom1_Pro.Models
{
    public partial class BillDetail
    {
        public Guid Id { get; set; }
        public Guid? IdBill { get; set; }
        public Guid? IdProductDetail { get; set; }
        public int? SoLuong { get; set; }
        public decimal? DonGia { get; set; }
        public int? TrangThai { get; set; }
        public virtual Bill Bill { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }
    }
}
