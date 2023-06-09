using Nhom1_Pro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class BillDetailView
    {
        public Guid Id { get; set; }
        public Guid? IdBill { get; set; }
        public Guid? IdProductDetail { get; set; }
        public string Ma { get; set; }
        public string? Ten { get; set; }
        public int? SoLuong { get; set; }
        public decimal? DonGia { get; set; }
        public int? TrangThai { get; set; }
    }
}
