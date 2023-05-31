using AppData.IRepositories;
using AppData.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nhom1_Pro.Models;

namespace AppView.Controllers
{
    public class VoucherViewController : Controller
    {
        // GET: VoucherController1
        private readonly IAllRepo<Voucher> allRepo;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<Voucher> vouchers;

        public VoucherViewController()
        {
            vouchers = dbContextModel.Vouchers;
            AllRepo<Voucher> all = new AllRepo<Voucher>(dbContextModel, vouchers);
            allRepo = all;
        }

        public async Task<IActionResult> IndexAsync()
        {
            string apiUrl = "https://localhost:7280/api/Voucher/GetVoucher";
            var httpclient = new HttpClient();
            var reponse = await httpclient.GetAsync(apiUrl);
            string apidata = await reponse.Content.ReadAsStringAsync();
            List<Voucher> voucherslst = JsonConvert.DeserializeObject<List<Voucher>>(apidata);
            return View(voucherslst);
        }

        // GET: VoucherController1/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: VoucherController1/Create
        public async Task<ActionResult> CreateAsync()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync(Voucher voucher)
        {

            string apiUrl = $"https://localhost:7280/api/Voucher/AddVoucher?ma={voucher.Ma}&loaihinhkm={voucher.LoaiHinhKm}&mucuudai={voucher.MucUuDai}&phamvi={voucher.PhamVi}&dieukien={voucher.DieuKien}&soluongton={voucher.SoLuongTon}&solansudung={voucher.SoLanSuDung}&ngaybatdau={voucher.NgayBatDau}&ngayketthuc={voucher.NgayKetThuc}&trangthai={voucher.TrangThai}";
            var httpclient = new HttpClient();
            var reponse = await httpclient.PostAsync(apiUrl, null);
                return RedirectToAction("Index");
        }
        // GET: VoucherController1/Edit/5
        public async Task<ActionResult> EditAsync(Guid id)
        {
            var a = allRepo.GetAll().FirstOrDefault(x => x.Id == id);
            return View(a);
        }

        // POST: VoucherController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Voucher voucher)
        {
            string apiUrl = $" https://localhost:7280/api/Voucher/{voucher.Id}?ma={voucher.Ma}&loaihinhkm={voucher.LoaiHinhKm}&mucuudai={voucher.MucUuDai}&phamvi={voucher.PhamVi}&dieukien={voucher.DieuKien}&soluongton={voucher.SoLuongTon}&solansudung={voucher.SoLanSuDung}&ngaybatdau={voucher.NgayBatDau}&ngayketthuc={voucher.NgayKetThuc}&trangthai={voucher.TrangThai}";
            var httpclient = new HttpClient();
            var reponse = await httpclient.PutAsync(apiUrl, null);
            return RedirectToAction("Index");
        }

        // GET: VoucherController1/Delete/5
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var a = allRepo.GetAll().FirstOrDefault(x => x.Id == id);
            return View(a);
        }

        // POST: VoucherController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(Voucher a)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Voucher/{a.Id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return RedirectToAction("Index");
        }
    }
}
