using AppView.IServices;
using AppView.Models;
using AppView.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppView.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductDetailService _productDetail;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _productDetail = new ProductDetailService();
        }

        public async Task<IActionResult> Index()
        {
            var listSanPham = (await _productDetail.GetAll()).GroupBy(item => new { item.Material, item.TypeProduct, item.Name }).Select(item => item.First()).ToList();
            return View(listSanPham);
        }


        public async Task<IActionResult> SanPhamNguoiDung()
        {
            var listSanPham = (await _productDetail.GetAll()).GroupBy(item => new { item.Material, item.TypeProduct, item.Name}).Select(item => item.First()).ToList();
            return View(listSanPham);
        }

        public async Task<ActionResult> GetListProductNguoiDung()
        {
            try
            {
                var listSanPham =(await _productDetail.GetAll()).GroupBy(item => new { item.Material, item.TypeProduct, item.Name }).Select(item => item.First()).ToList();
                return PartialView("_PatialViewListSPNguoiDung", listSanPham);
            }
            catch (HttpRequestException)
            {
                return BadRequest();
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}