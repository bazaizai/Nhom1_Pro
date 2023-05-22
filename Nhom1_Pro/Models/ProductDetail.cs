using System;
using System.Collections.Generic;

namespace Nhom1_Pro.Models
{
    public partial class ProductDetail
    {
        public Guid Id { get; set; }
        public Guid? IdProduct { get; set; }
        public Guid? IdColor { get; set; }
        public Guid? IdSize { get; set; }
        public Guid? IdTypeProduct { get; set; }
        public Guid? IdMaterial { get; set; }
        public string? BaoHanh { get; set; }
        public string? MoTa { get; set; }
        public int? SoLuongTon { get; set; }
        public decimal? GiaNhap { get; set; }
        public decimal? GiaBan { get; set; }
        public int? TrangThai { get; set; }

        public virtual Product Product { get; set; }

        public virtual Color Color { get; set; }

        public virtual Size Size { get; set; }

        public virtual TypeProduct TypeProduct { get; set; }

        public virtual Material Material { get; set; }

        public virtual IEnumerable<BillDetail> BillDetail { get; set; }

        public virtual IEnumerable<Image> Image { get; set; }

        public virtual IEnumerable<SaleDetail> DetailSale { get; set; }

        public virtual IEnumerable<CartDetail> CartDetail { get; set; }
    }
}
