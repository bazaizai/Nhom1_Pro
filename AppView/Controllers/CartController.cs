using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using AppView.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom1_Pro.Models;
using System.Drawing;
using System.Globalization;

namespace AppView.Controllers
{
    public class CartController : Controller
    {
        private readonly IAllRepo<Cart> repos;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<Cart> Carts;
        CartServices cartServices;
        CartDetailServices cartDetailServices;
        UserServices userServices;
        private ProductDetailService productDetailService;
        public CartController()
        {
            Carts = dbContextModel.Carts;
            AllRepo<Cart> all = new AllRepo<Cart>(dbContextModel, Carts);
            repos = all;
            cartServices = new CartServices();
            cartDetailServices = new CartDetailServices();
            userServices = new UserServices();
            productDetailService = new ProductDetailService();
        }
        // GET: CartController
        public async Task<ActionResult> GetAllCart()
        {
            var a = await cartServices.GetAllCart();
            return View(a);
        }

        // GET: CartController/Details/5
        public ActionResult Details(Guid id)
        {
            var cart = repos.GetAll().FirstOrDefault(c => c.UserID == id);
            return View(cart);
        }

        // GET: CartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Guid idUser, string mota)
        {
            await cartServices.AddCart(idUser, mota);
            return RedirectToAction("GetAllCart");
        }

        // GET: CartController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var cart = repos.GetAll().FirstOrDefault(c => c.UserID == id);
            return View(cart);
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid idUser, string mota)
        {
            await cartServices.Edit(idUser, mota);
            return RedirectToAction("GetAllCart");
        }

        // GET: CartController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            await cartDetailServices.RemoveItem(id);
            return RedirectToAction("ShowCart");
        }
        public async Task<ActionResult> DeleteAll()
        {
            var acc = SessionServices.GetObjFromSession(HttpContext.Session, "acc").TaiKhoan;
            var idCart = (await userServices.GetAllUser()).FirstOrDefault(c => c.TaiKhoan == acc).Id;
            List<CartViewModel> cartDetails = (await cartDetailServices.GetAllAsync()).Where(x => x.IdUser == idCart).ToList();
            foreach (var item in cartDetails)
            {
                cartDetailServices.RemoveItem(item.Id);
            }
            return RedirectToAction("ShowCart");
        }
        public async Task<ActionResult> ShowCart()
        {
            var acc = SessionServices.GetObjFromSession(HttpContext.Session, "acc").TaiKhoan;
            var idCart = (await userServices.GetAllUser()).FirstOrDefault(c => c.TaiKhoan == acc).Id;
            var a = (await cartDetailServices.GetAllAsync()).Where(c => c.IdUser == idCart).ToList();
            return View(a);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCartUser(CartViewModel model)
        {
            var acc = SessionServices.GetObjFromSession(HttpContext.Session, "acc");
            var product = await productDetailService.GetById(model.IdProduct);

            if (acc == null)
            {

                TempData["dangnhap"] = "Bạn phải đăng nhập";
            }
            else
            {

                var IdCart = (await userServices.GetAllUser()).FirstOrDefault(c => c.TaiKhoan == acc.TaiKhoan).Id;
                var existing = (await cartDetailServices.GetAllAsync()).FirstOrDefault(x => x.IdProduct == product.Id && x.IdUser == IdCart);
                if (existing != null)
                {
                    //Kiểm tra số lượng vs số lượng tồn
                    if (existing.SoLuongCart + model.SoLuongCart <= product.SoLuongTon)
                    {
                        // Nếu sản phẩm đã có trong giỏ hàng thì tăng số lượng lên 1
                        existing.SoLuongCart += model.SoLuongCart;
                    }
                    else
                    {
                        TempData["quantityCartUser"] = "Số lượng bạn chọn đã đạt mức tối đa của sản phẩm này";

                        existing.SoLuongCart = Convert.ToInt32(product.SoLuongTon);
                    }
                    if (existing.IdSaleDetai!=null)
                    {
                        if (existing.LoaiHinhKm=="%")
                        {
                            var cartdetail = new CartDetail()
                            {
                                Id = existing.Id,
                                DetailProductID = product.Id,
                                UserID = IdCart,

                                Dongia = Convert.ToDecimal(existing.GiaBan)- Convert.ToDecimal(existing.GiaBan)* Convert.ToDecimal(existing.MucGiam)/100,
                                Soluong = existing.SoLuongCart,
                            };
                            cartDetailServices.EditItem(cartdetail);
                        }
                        if (existing.LoaiHinhKm == "Đ")
                        {
                            var cartdetail = new CartDetail()
                            {
                                Id = existing.Id,
                                DetailProductID = product.Id,
                                UserID = IdCart,

                                Dongia = Convert.ToDecimal(existing.GiaBan) - Convert.ToDecimal(existing.MucGiam) ,
                                Soluong = existing.SoLuongCart,
                            };
                            cartDetailServices.EditItem(cartdetail);
                        }
                       
                    }
                    else
                    {
                        var cartdetail = new CartDetail()
                        {
                            Id = existing.Id,
                            DetailProductID = product.Id,
                            UserID = IdCart,

                            Dongia = Convert.ToDecimal(existing.GiaBan),
                            Soluong = existing.SoLuongCart,
                        };
                        cartDetailServices.EditItem(cartdetail);
                    }
                    
                }
                else
                {
                    var cartDetails = new CartDetail();
                    cartDetails.Id = Guid.NewGuid();
                    cartDetails.UserID = IdCart;
                    cartDetails.DetailProductID = model.IdProduct;
                    cartDetails.Soluong = model.SoLuongCart;
                    cartDetails.Dongia = Convert.ToDecimal(product.GiaBan);
                    cartDetails.TrangThai = 0;
                    cartDetailServices.AddItemAsync(cartDetails);
                }

            }
            return RedirectToAction("ChiTietSP", "Home", new { id = product.Id });

        }
        [HttpPost]
        public async Task<IActionResult> CapNhatSoLuong(Guid Idcart, int soLuong, Guid productId)
        {
            var apiUrl = $"https://localhost:7280/api/CartDetails/udpade?id={Idcart}&soluong={soLuong}";
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsync(apiUrl, null);
            var price = Convert.ToDecimal((await productDetailService.GetById(productId)).GiaBan * soLuong);
            var formattedPrice = string.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:N0}đ", price);
            var acc = SessionServices.GetObjFromSession(HttpContext.Session, "acc").TaiKhoan;
            var idCart = (await userServices.GetAllUser()).FirstOrDefault(c => c.TaiKhoan == acc).Id;
            var a = (await cartDetailServices.GetAllAsync()).Where(c => c.IdUser == idCart).ToList();
            decimal? sum = 0;
            foreach (var item in a)
            {
                sum += Convert.ToDecimal(item.SoLuongCart) * item.GiaBan;
            }
            return Json(new { price = formattedPrice, sum = sum });
        }

    }
}
