using AppData.IRepositories;
using AppData.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nhom1_Pro.Models;

namespace AppView.Controllers
{
    public class SaleController : Controller
    {
        private readonly IAllRepo<Sale> repos;
        DBContextModel context = new DBContextModel();
        DbSet<Sale> sale;
        public SaleController()
        {
            sale = context.Sales;
            AllRepo<Sale> all = new AllRepo<Sale>(context, sale);
            repos = all;
        }
        // GET: SaleController
        public async Task<IActionResult> GetAllSale()
        {
            string apiUrl = "https://localhost:7280/api/Sale";
            var httpClient = new HttpClient(); // tạo ra để callApi
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var sales = JsonConvert.DeserializeObject<List<Sale>>(apiData);
            return View(sales);
        }

        // GET: SaleController/Details/5
        public ActionResult DetailSale(Guid id)
        {
            var sale = repos.GetAll().FirstOrDefault(x => x.Id == id);
            return View(sale);
        }

        // GET: SaleController/Create
        public ActionResult CreateSale()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSale(string ma, string ten, DateTime ngaybatdau, DateTime ngayketthuc, string LoaiHinhKm, string mota, decimal mucgiam, int trangthai)
        {
            string apiUrl = $"https://localhost:7280/api/Sale?ma={ma}&ten={ten}&ngaybatdau={ngaybatdau}&ngayketthuc={ngayketthuc}&LoaiHinhKm={LoaiHinhKm}&mota={mota}&mucgiam={mucgiam}&trangthai={trangthai}";
            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync(apiUrl, null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllSale");
            }

            return View();
        }



        public async Task<IActionResult> DeleteSale(Guid id)
        {
            string apiUrl = $"https://localhost:7280/api/Sale/{id}";
            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllSale");
            }

            return View();
        }
        public IActionResult EditSale(Guid id)
        {
            var sp = repos.GetAll().FirstOrDefault(x => x.Id == id);

            if (sp == null)
            {
                return NotFound();
            }

            return View(sp);
        }

        [HttpPost]
        public async Task<IActionResult> EditSale(Guid id, string ma, string ten, DateTime ngaybatdau, DateTime ngayketthuc, string LoaiHinhKm, string mota, decimal mucgiam, int trangthai)
        {
            string apiUrl = $"https://localhost:7280/api/Sale/{id}?ma={ma}&ten={ten}&ngaybatdau={ngaybatdau}&ngayketthuc={ngayketthuc}&LoaiHinhKm={LoaiHinhKm}&mota={mota}&mucgiam={mucgiam}&trangthai={trangthai}";
            var httpClient = new HttpClient();

            var content = new StringContent(string.Empty);

            var response = await httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllSale");
            }

            return BadRequest();
        }
    }
}
