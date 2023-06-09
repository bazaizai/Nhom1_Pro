using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using AppView.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom1_Pro.Models;
using System.Drawing;

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
            await cartServices.DeleteCart(id);
            return RedirectToAction("GetAllCart");
        }

        public async Task<ActionResult> ShowCart()
        {
            var acc = HttpContext.Session.GetString("acc");
            var idCart = (await userServices.GetAllUser()).FirstOrDefault(c => c.TaiKhoan == acc).Id;
            var b = await cartDetailServices.GetAllAsync();
            var a = (await cartDetailServices.GetAllAsync()).Where(c => c.IdUser == idCart).ToList();
            return View(a);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddToCartUser(CartViewModel model)
        {
            var acc = HttpContext.Session.GetString("acc");
            var IdCart = (await userServices.GetAllUser()).FirstOrDefault(c => c.TaiKhoan == acc).Id;
            var product = (await productDetailService.GetAll()).FirstOrDefault(c=>c.Id== model.IdProduct);
            var existing = (await cartDetailServices.GetAllAsync()).FirstOrDefault(x => x.IdProduct == product.Id && x.IdUser == IdCart);
            // if (existing != null)
            // {
            //     //Kiểm tra số lượng vs số lượng tồn
            //     if (existing.Soluong + model.SoLuongCart <= product.SoLuongTon)
            //     {
            //         // Nếu sản phẩm đã có trong giỏ hàng thì tăng số lượng lên 1
            //         existing.Soluong += model.SoLuongCart;
            //     }
            //     else
            //     {
            //         TempData["quantityCartUser"] = "Số lượng bạn chọn đã đạt mức tối đa của sản phẩm này";
            //         existing.Soluong = product.SoLuongTon;
            //     }
            //     cartDetailServices.UpdateCartDetail(existing);
            // }
            // else
            // {
                var cartDetails = new CartDetail();
                cartDetails.Id = Guid.NewGuid();
                cartDetails.UserID = IdCart;
                cartDetails.DetailProductID = model.IdProduct;
                cartDetails.Soluong = model.SoLuongCart;
                cartDetails.Dongia = Convert.ToDecimal(product.GiaBan);
                cartDetails.TrangThai = 0;
                cartDetailServices.AddItemAsync(cartDetails);
            //}
            // List<CartDetail> cartDetail = cartDetailServices.GetAllCartDetail().Where(x => x.UserID == IdCart).ToList();
            // HttpContext.Session.SetString("itemCount", cartDetail.Count().ToString());
            return RedirectToAction("Index", "Home", new { id = product.Id });
        }
    }
}
