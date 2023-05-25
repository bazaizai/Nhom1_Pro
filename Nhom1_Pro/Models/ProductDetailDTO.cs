using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class ProductDetailDTO
    {

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public string? TypeProduct { get; set; }
        public string? Material { get; set; }
        public string? BaoHanh { get; set; }
        public string? MoTa { get; set; }
        public int? SoLuongTon { get; set; }
        public decimal? GiaNhap { get; set; }
        public decimal? GiaBan { get; set; }
        public int? TrangThai { get; set; }
        public List<string> ListImage { get; set; }
    }
}
