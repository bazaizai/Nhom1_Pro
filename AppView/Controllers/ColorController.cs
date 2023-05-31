using AppData.IRepositories;
using AppData.Repositories;
using AppView.IServices;
using AppView.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using System.Net.Http;

namespace AppView.Controllers
{
    public class ColorController : Controller
    {
        private readonly IAllRepo<Color> allRepo;
        private readonly IColorServices colorServices;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<Color> Colors;
        public ColorController()
        {
            Colors = dbContextModel.Colors;
            colorServices= new ColorServices();
            AllRepo<Color> all = new AllRepo<Color>(dbContextModel, Colors);
            allRepo = all;
        }
        public async Task<IActionResult> GetAllColorAsync(string search)
        {
            var Color = await colorServices.GetAllColor();
            if (search == null)
            {
                return View(Color);
            }
            else
            {
                var colors = Color.Where(c => c.Ten.ToUpper().Contains(search.ToUpper())).ToList();
                return View(colors);
            }

        }
        [HttpGet]
        public IActionResult DetailsAsync(Guid id)
        {
            var Color = allRepo.GetAll().FirstOrDefault(c => c.Id == id);
            return View(Color);
        }
        [HttpGet]
        public IActionResult EditAsync(Guid id)
        {
            var Color = allRepo.GetAll().FirstOrDefault(c => c.Id == id);
            return View(Color);
        }
        public async Task<IActionResult> EditAsync(Guid id, string ten, int trangthai)
        {
            if (await colorServices.EditColor(id,ten,trangthai) == true)
            {
                return RedirectToAction("GetAllColor");
            }
            else return BadRequest();
          
        }
        [HttpGet]
        public IActionResult DeleteAsync(Guid id)
        {
            var Color = allRepo.GetAll().FirstOrDefault(c => c.Id == id);
            return View(Color);
        }
        public async Task<IActionResult> DeleteAsync(Color color)
        {
            if (await colorServices.DeleteColor(color.Id) == true)
            {
                return RedirectToAction("GetAllColor");
            }
            else return BadRequest();
        }
        public IActionResult CreateAsync()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(string ten)
        {
            if (await colorServices.AddColor(ten) == true)
            {
                return RedirectToAction("GetAllColor");
            }
            else return BadRequest();
        }

    }
}
