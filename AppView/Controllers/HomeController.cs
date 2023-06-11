﻿using AppView.IServices;
using AppView.Models;
using AppView.Services;
using Microsoft.AspNetCore.Mvc;
using Nhom1_Pro.Models;
using System.Diagnostics;
using System.Diagnostics.Metrics;

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
            roleServices = new RoleServices();
            _productDetail = new ProductDetailService();
        }
        //public IActionResult CreateUser()
        //{
        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            //Tu tao role admin neu khong co
            Role RoleAdmin = (await roleServices.GetAllRole()).FirstOrDefault(c => c.Id == Guid.Parse("92917570-dd9d-445c-9373-40b4640c2ac0"));
            if (RoleAdmin == null)
            {
                Role role = new Role()
                {
                    Id = Guid.Parse("92917570-dd9d-445c-9373-40b4640c2ac0"),
                    Ten = "Admin",
                    TrangThai = 0
                };
                roleServices.AddRoleGuest(role.Id, role.Ten, role.TrangThai);
            }
            var taikhoan = (await userServices.GetAllUser()).FirstOrDefault(c => c.TaiKhoan == user.TaiKhoan);
            var Email = (await userServices.GetAllUser()).FirstOrDefault(c => c.Email == user.Email);
            var Sdt = (await userServices.GetAllUser()).FirstOrDefault(c => c.Sdt == user.Sdt);
            // Check if the TaiKhoan is already taken by another user
            if (taikhoan != null)
            {
                TempData["MessageForCreate"] = "Tài khoản đã tồn tại";
                return RedirectToAction("Index");
            }

            // Check if the Email is already taken by another user
            if (Email != null)
            {
                TempData["MessageForCreate"] = "Email đã được sử dụng";
                return RedirectToAction("Index");
            }

            // Check if the Sdt is already taken by another user
            if (Sdt != null)
            {
                TempData["MessageForCreate"] = "Số điện thoại đã được sử dụng";
                return RedirectToAction("Index");
            }

            // If no duplicates were found, continue with creating the new user
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
                        Mota = "0",
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
                        Mota = "0",
                        TrangThai = 0
                    };
                    cartServices.AddCart(cart.UserID, cart.Mota);
                    TempData["MessageForCreate"] = "Đăng ký thành công";
                }
                else
                {
                    TempData["MessageForCreate"] = "Đăng kí thất bại";
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            //var listSanPham = (await _productDetail.GetAll()).GroupBy(item => new { item.Material, item.TypeProduct, item.Name }).Select(item => item.First()).ToList();
            return View();
        }

        public async Task<IActionResult> Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var data = (await userServices.GetByLogin(username, password));
                //add Session
                if (data != null)
                {
                    SessionServices.SetObjToSession(HttpContext.Session, "acc", data);
                    TempData["MessageForLogin"] = "Đăng nhập thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MessageForLogin"] = "Đăng nhập thất bại";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("acc");
            TempData["MessageForLogout"] = "Đăng xuất thành công";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SanPhamNguoiDung()
        {
            var listSanPham = (await _productDetail.GetAll()).GroupBy(item => new { item.Material, item.TypeProduct, item.Name }).Select(item => item.First()).ToList();
            return View(listSanPham);
        }

        public async Task<ActionResult> GetListProductNguoiDung()
        {
            try
            {
                var listSanPham = (await _productDetail.GetAll()).GroupBy(item => new { item.Material, item.TypeProduct, item.Name }).Select(item => item.First()).ToList();
                return PartialView("_PatialViewListSPNguoiDung", listSanPham);
            }
            catch (HttpRequestException)
            {
                return BadRequest();
            }
        }
        public async Task<IActionResult> ChiTietSP(Guid id)
        {
            var product = await _productDetail.GetById(id);
            return View(product);
        }
        public IActionResult About()
        {
            return View();
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