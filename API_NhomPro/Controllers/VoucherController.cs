using AppData.IRepositories;
using AppData.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom1_Pro.Models;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IAllRepo<Voucher> allRepo;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<Voucher> vouchers;

        public VoucherController()
        {
            vouchers = dbContextModel.Vouchers;
            AllRepo<Voucher> all = new AllRepo<Voucher>(dbContextModel, vouchers);
            allRepo = all;
        }
        [HttpGet("GetVoucher")]
        public IEnumerable<Voucher> GetAll()
        {
            return allRepo.GetAll();
        }
        [HttpPost("AddVoucher")]
        public bool AddVoucher(string ma, string loaihinhkm, decimal mucuudai, string phamvi, string dieukien, int soluongton, int solansudung, DateTime ngaybatdau, DateTime ngayketthuc, int trangthai)
        {
            var voucher = new Voucher()
            {
                Id = Guid.NewGuid(),
                Ma = ma,
                LoaiHinhKm = loaihinhkm,
                MucUuDai = mucuudai,
                PhamVi = phamvi,
                DieuKien = dieukien,
                SoLuongTon = soluongton,
                SoLanSuDung = solansudung,
                NgayBatDau = ngaybatdau,
                NgayKetThuc = ngayketthuc,
                TrangThai = trangthai
            };
            return allRepo.AddItem(voucher);
        }
        [HttpPut("{id}")]
        public bool UpdateVoucher(Guid id, string ma, string loaihinhkm, decimal mucuudai, string phamvi, string dieukien, int soluongton, int solansudung, DateTime ngaybatdau, DateTime ngayketthuc, int trangthai)
        {
            var voucher = new Voucher()
            {
                Id = id,
                Ma = ma,
                LoaiHinhKm = loaihinhkm,
                MucUuDai = mucuudai,
                PhamVi = phamvi,
                DieuKien = dieukien,
                SoLuongTon = soluongton,
                SoLanSuDung = solansudung,
                NgayBatDau = ngaybatdau,
                NgayKetThuc = ngayketthuc,
                TrangThai = trangthai
            };
            return allRepo.EditItem(voucher);
        }
        [HttpDelete("{id}")]
        public bool DeleteVoucher(Guid id)
        {
            return allRepo.RemoveItem(allRepo.GetAll().FirstOrDefault(c => c.Id == id));
        }
    }
}
