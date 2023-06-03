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
        public CartController()
        {
            Carts = dbContextModel.Carts;
            AllRepo<Cart> all = new AllRepo<Cart>(dbContextModel, Carts);
            repos = all;
            cartServices = new CartServices();
            cartDetailServices = new CartDetailServices();
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
            var a = await cartDetailServices.GetAllAsync();
            return View(a);
        }
    }
}
