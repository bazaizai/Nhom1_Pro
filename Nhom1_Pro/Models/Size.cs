using System;
using System.Collections.Generic;

namespace Nhom1_Pro.Models
{
    public partial class Size
    {
        public Guid Id { get; set; }
        public string Ma { get; set; }
        public string Size1 { get; set; }
        public decimal Cm { get; set; }
        public int TrangThai { get; set; }
        public virtual List<ProductDetail> ProductDetail { get; set; }
    }
}
