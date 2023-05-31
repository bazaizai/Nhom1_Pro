using AppData.IRepositories;
using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nhom1_Pro.Models;

namespace AppView.Controllers
{
    public class BillController : Controller
    {
        public IAllRepo<Bill> allRepo;
        public BillController()
        {
            allRepo = new AllRepo<Bill>();
        }
        public async Task<IActionResult> GetAllBill()
        {
            string apiUrl = "https://localhost:7280/api/Bill";
            var httpClient = new HttpClient(); 
            var response = await httpClient.GetAsync(apiUrl);

            string apiData = await response.Content.ReadAsStringAsync();

            var bills = JsonConvert.DeserializeObject<List<Bill>>(apiData);
            return View(bills);
        }
      
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Guid idUser, Guid idVoucher, string ma, DateTime ngayTao, DateTime ngayThanhToan, DateTime ngayShip, DateTime ngayNhan,
            string tenNguoiNhan, string diaChi, string sdt, int tongTien, int soTienGiam, int tienShip, string moTa, int trangThai)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Bill?idUser={idUser}&idVoucher={idVoucher}&ma={ma}&ngayTao={ngayTao}&ngayThanhToan={ngayThanhToan}&ngayShip={ngayShip}&ngayNhan={ngayNhan}" +
                $"&tenNguoiNhan={tenNguoiNhan}&diaChi={diaChi}&sdt={sdt}&tongTien={tongTien}&soTienGiam={soTienGiam}&tienShip={tienShip}&moTa={moTa}&trangThai={trangThai}";
            var response = await httpClient.PostAsync(apiUrl, null);
            return RedirectToAction("GetAllBill");
        }
        public IActionResult Details(Guid id)
        {
            DBContextModel dBContextModel = new DBContextModel();
            var a = dBContextModel.Bills.Find(id);
            return View(a);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            DBContextModel dBContextModel = new DBContextModel();
            var a = dBContextModel.Bills.Find(id);
            return View(a);
        }
        [HttpPost]
        public async Task<IActionResult> Edit( Guid id, Guid idUser, Guid idVoucher, string ma, DateTime ngayTao, DateTime ngayThanhToan, DateTime ngayShip, DateTime ngayNhan,
            string tenNguoiNhan, string diaChi, string sdt, int tongTien, int soTienGiam, int tienShip, string moTa, int trangThai)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Bill/{id}?idUser={idUser}&idVouCher={idVoucher}&ma={ma}&ngayTao={ngayTao}&ngayThanhToan={ngayThanhToan}&ngayShip={ngayShip}&ngayNhan={ngayNhan}" +
                $"&tenNguoiNhan={tenNguoiNhan}&diaChi={diaChi}&sdt={sdt}&tongTien={tongTien}&soTienGiam={soTienGiam}&tienShip={tienShip}&moTa={moTa}&trangThai={trangThai}";
            var response = await httpClient.PutAsync(apiUrl, null);
            return RedirectToAction("GetAllBill");
        }
        
        public async Task<IActionResult> Delete(Guid id)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Bill/{id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return RedirectToAction("GetAllBill");
        }
    }
}
