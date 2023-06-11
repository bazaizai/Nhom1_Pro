using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessageResourceName = "Số lượng không được để trống.")]
        public int? SoLuongTon { get; set; }
        [Required(ErrorMessage = "Giá nhập không được để trống.")]
        public decimal? GiaNhap { get; set; }
        [Required(ErrorMessage ="Giá bán không được để trống")]
        public decimal? GiaBan { get; set; }
        public int? TrangThai { get; set; }
        public string? LinkImage { get; set; }
        //sale
        public DateTime? NgayKetThuc { get; set; }
        public int? TrangThaiSale { get; set; }
        public string? LoaiHinhKm { get; set; }
        public decimal? MucGiam { get; set; }
        public string? TenSale { get; set; }
        public Guid? IdSale { get; set; }
        public Guid? IdSaleDetai { get; set; }
    }
}
