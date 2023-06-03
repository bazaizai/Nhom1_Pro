using AppView.IServices;
using AppView.Models;
using AppView.Services;
using Microsoft.AspNetCore.Mvc;
using Nhom1_Pro.Models;
using System.Diagnostics;

namespace AppView.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUserServices userServices;
        private readonly ICartServices cartServices;
        private readonly IRoleServices roleServices;
        private readonly IProductDetailService _productDetail;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            userServices = new UserServices();
            cartServices = new CartServices();
            roleServices= new RoleServices();
            _productDetail = new ProductDetailService();
        }
        //public IActionResult CreateUser()
        //{
        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            user.Id = Guid.NewGuid();
            user.TrangThai = 0;
            Role Role = (await roleServices.GetAllRole()).FirstOrDefault(c => c.Id == Guid.Parse("f79544bc-fdc7-47cf-9f92-41cc05fb381f"));
            if (Role == null)
            {
                Role role = new Role()
                {
                    Id = Guid.Parse("f79544bc-fdc7-47cf-9f92-41cc05fb381f"),
                    Ten = "Guest",
                    TrangThai = 0
                };
                roleServices.AddRoleGuest(role.Id, role.Ten, role.TrangThai);
                user.IdRole = role.Id;
                if (await userServices.AddUser(user) == true)
                {
                    Cart cart = new Cart()
                    {
                        UserID = user.Id,
                        Mota = null,
                        TrangThai = 0
                    };
                    cartServices.AddCart(cart.UserID, cart.Mota);
                    TempData["MessageForCreate"] = "Đăng kí thành công";
                }
                else
                {
                    TempData["MessageForCreate"] = "Đăng kí thất bại";
                }
                return RedirectToAction("Index");

            }
            else
            {
                user.IdRole = Guid.Parse("f79544bc-fdc7-47cf-9f92-41cc05fb381f");
                if (await userServices.AddUser(user) == true)
                {
                    Cart cart = new Cart()
                    {
                        UserID = user.Id,
                        Mota = null,
                        TrangThai = 0
                    };
                    cartServices.AddCart(cart.UserID, cart.Mota);
                    TempData["MessageForCreate"] = "Đăng ký thành công";
                }
                else
                {
                    TempData["MessageForCreate"] = "Đăng kí thất bại";
                }
                return RedirectToAction("Index");
            }
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