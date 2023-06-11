using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class productSale
    {
        public string? TenSP { get; set; }

        public int? SoLuongTon { get; set; }
        public decimal? GiaNhap { get; set; }
        public decimal? GiaBan { get; set; }
        public string? LoaiHinhKm { get; set; }
        public decimal? MucGiam { get; set; }
        public string? TenSale { get; set; }
        public Guid Id { get; set; }
        public Guid? IdSale { get; set; }
        
        public string? MoTa { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? TrangThaiSale { get; set; }

    }
}
