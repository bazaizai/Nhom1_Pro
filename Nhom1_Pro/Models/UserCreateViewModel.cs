using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class UserCreateViewModel
    {
        public Guid IdRole { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public int GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string Sdt { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public string TaiKhoan { get; set; }
        public int TrangThai { get; set; }
    }
}
