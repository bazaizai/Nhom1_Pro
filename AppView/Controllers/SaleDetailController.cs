using AppData.IRepositories;
using AppData.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nhom1_Pro.Models;

namespace AppView.Controllers
{
    public class SaleDetailController : Controller
    {
        private readonly IAllRepo<SaleDetail> repos;
        private readonly IAllRepo<Product> repoproduct;
        DBContextModel context = new DBContextModel();
        DbSet<SaleDetail> saleDetail;
        public SaleDetailController()
        {
            saleDetail = context.DetailSales;
            AllRepo<SaleDetail> all = new AllRepo<SaleDetail>(context, saleDetail);
            repos = all;
        }

        public async Task<IActionResult> GetAllSaleDetail()
        {
            string apiUrl = "https://localhost:7280/api/SaleDetail";
            var httpClient = new HttpClient(); // tạo ra để callApi
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var SaleDetails = JsonConvert.DeserializeObject<List<SaleDetail>>(apiData);
            return View(SaleDetails);
        }
        public IActionResult DetailSaleDetail(Guid id)
        {
            var sp = repos.GetAll().FirstOrDefault(x => x.Id == id);
            return View(sp);
        }


        public IActionResult CreateSaleDetail()
        {
            var listsp = context.ProductDetails.Include("Product").ToList();
            IEnumerable<SelectListItem> salesList = new SelectList(context.Sales, "Id", "Ten");
            ViewBag.SaleList = salesList;
            IEnumerable<SelectListItem> productDetailsList = listsp.Select(pd => new SelectListItem
            {
                Value = pd.Id.ToString(),
                Text = pd.Product.Ten // Truy cập thuộc tính navigation để lấy tên sản phẩm
            });

            ViewBag.ProductDetailsList = productDetailsList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSaleDetail(Guid IdSale,Guid IdChiTietSp, string mota, int trangthai)
        {
            
   
            string apiUrl = $"https://localhost:7280/api/SaleDetail?mota={mota}&trangthai={trangthai}&IdSale={IdSale}&IdChiTietSp={IdChiTietSp}";
            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync(apiUrl, null);


            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllSaleDetail");
            }
            return View();
        }


        public async Task<IActionResult> DeleteSaleDetail(Guid id)
        {
            string apiUrl = $"https://localhost:7280/api/SaleDetail/{id}";
            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllSaleDetail");
            }

            return View();
        }
        public IActionResult EditSaleDetail(Guid id)
        {
            IEnumerable<SelectListItem> salesList = new SelectList(context.Sales, "Id", "Ten");
            ViewBag.SaleList = salesList;

            IEnumerable<SelectListItem> productDetailsList = new SelectList(context.ProductDetails, "Id", "Id");
            ViewBag.ProductDetailsList = productDetailsList;
            ViewBag.SaleList = salesList;
            var sp = repos.GetAll().FirstOrDefault(x => x.Id == id);

            if (sp == null)
            {
                return NotFound();
            }

            return View(sp);
        }

        [HttpPost]
        public async Task<IActionResult> EditSaleDetail(Guid id, Guid IdSale, Guid IdChiTietSp, string mota, int trangthai)
        {
            string apiUrl = $"https://localhost:7280/api/SaleDetail/{id}?mota={mota}&trangthai={trangthai}&IdSale={IdSale}&IdChiTietSp={IdChiTietSp}";
            var httpClient = new HttpClient();

            var content = new StringContent(string.Empty);

            var response = await httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllSaleDetail");
            }

            return BadRequest();
        }
    }
}
