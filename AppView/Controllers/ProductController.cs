using AppData.IRepositories;
using AppData.Repositories;
using AppView.IServices;
using AppView.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using System.Text;

namespace AppView.Controllers
{
    public class ProductController : Controller
    {
        private readonly IAllRepo<Product> repos;
        DBContextModel context = new DBContextModel();
        DbSet<Product> product;
        Isalesevice ProductService;
        public ProductController()
        {
            product = context.Products;
            AllRepo<Product> all = new AllRepo<Product>(context, product);
            repos = all;
            ProductService = new ProductService();
        }
        
        public async Task<IActionResult> GetAllSanPham()
        {         
            var sanphams = await ProductService.GetAllSanPham();
            return View(sanphams);
        }
        public IActionResult DetailSp(Guid id)
        {
            var sp = repos.GetAll().FirstOrDefault(x => x.Id == id);
            return View(sp);
        }
       

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product p)
        {
            if (await ProductService.CreateSanPham(p))
            {
                return RedirectToAction("GetAllSanPham");
            }
            else return BadRequest();
        }


        public async Task<IActionResult> DeleteSP(Guid id)
        {
            if (await ProductService.DeleteSanPham(id))
            {
                return RedirectToAction("GetAllSanPham");
            }
            else return BadRequest();
        }
        public IActionResult EditSp(Guid id)
        {
            var sp = repos.GetAll().FirstOrDefault(x => x.Id == id);

            if (sp == null)
            {
                return NotFound(); 
            }

            return View(sp);
        }

        [HttpPost]
        public async Task<IActionResult> EditSp(Product p)
        {
            if (await ProductService.EditSanPham(p))
            {
                return RedirectToAction("GetAllSanPham");
            }
            else return BadRequest();
        }



    }
}
