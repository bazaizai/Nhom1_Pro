using System;
using System.Collections.Generic;

namespace Nhom1_Pro.Models
{
    public partial class SaleDetail
    {
        public Guid Id { get; set; }
        public Guid? IdSale { get; set; }
        public Guid? IdChiTietSp { get; set; }
        public string? MoTa { get; set; }
        public int? TrangThai { get; set; }
        public virtual ProductDetail? ProductDetail { get; set; }
        public virtual Sale? Sale { get; set; }  
        

    }
}
