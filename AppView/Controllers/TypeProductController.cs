using AppData.IRepositories;
using AppData.Repositories;
using AppView.IServices;
using AppView.Services;
using Microsoft.AspNetCore.Mvc;
using Nhom1_Pro.Models;
using System;

namespace AppView.Controllers
{
    public class TypeProductController : Controller
    {
        public IAllRepo<TypeProduct> allRepo;
        public ITypeProductService productService;
        public TypeProductController()
        {
            allRepo = new AllRepo<TypeProduct>();
            productService = new TypeProductService();
        }
        public async Task<IActionResult> GetAllTypeProduct()
        {
            var a = await productService.GetAllTypeProductsAsync();
            return View(a);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TypeProduct typeProduct)
        {
            await productService.CreateTypeProductAsync(typeProduct);
            return RedirectToAction("GetAllTypeProduct");
        }
        public async Task<IActionResult> Details(Guid id)
        {

            var a = (await productService.GetAllTypeProductsAsync()).FirstOrDefault(x => x.Id == id);
            return View(a);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            DBContextModel dBContextModel = new DBContextModel();
            var a = dBContextModel.TypeProducts.Find(id);
            return View(a);
        }

        public async Task<IActionResult> Edit(TypeProduct typeProduct)
        {
            await productService.UpdateTypeProductAsync(typeProduct);
            return RedirectToAction("GetAllTypeProduct");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await productService.DeleteTypeProductAsync(id);
            return RedirectToAction("GetAllTypeProduct");
        }
    }
}
