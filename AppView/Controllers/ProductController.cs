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
      
        Isalesevice ProductService;
        public ProductController()
        {       
            ProductService = new ProductService();
        }
        
        public async Task<IActionResult> GetAllSanPham()
        {         
            var sanphams = await ProductService.GetAllSanPham();
            return View(sanphams);
        }
        public async Task<IActionResult> DetailSp(Guid id)
        {
            var sp = (await ProductService.GetAllSanPham()).FirstOrDefault(x => x.Id == id);
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
        public async Task<IActionResult> EditSpAsync(Guid id)
        {
            var sp = (await ProductService.GetAllSanPham()).FirstOrDefault(x => x.Id == id);

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
