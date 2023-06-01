using AppData.IRepositories;
using AppData.Repositories;
using AppView.IServices;
using AppView.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace AppView.Controllers
{
    public class SizeController : Controller
    {
        private readonly ISizeServices sizeServices;
        public SizeController()
        {
            sizeServices = new SizeServices();
        }
        public async Task<IActionResult> GetAllSizeAsync(string search)
        {
            var Size = await sizeServices.GetAllSize();
            if (search == null)
            {
                return View(Size);
            }
            else
            {
                var Sizes = Size.Where(c => c.Size1.ToUpper().Contains(search.ToUpper())).ToList();
                return View(Sizes);
            }

        }
        [HttpGet]
        public async Task<IActionResult> DetailsAsync(Guid id)
        {
            var Size = (await sizeServices.GetAllSize()).FirstOrDefault(c => c.Id == id);
            return View(Size);
        }
        [HttpGet]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var Size = (await sizeServices.GetAllSize()).FirstOrDefault(c => c.Id == id);
            return View(Size);
        }
        public async Task<IActionResult> EditAsync(Guid id, string ten, int trangthai, decimal cm)
        {
            if (await sizeServices.EditSize(id, ten, trangthai,cm) == true)
            {
                return RedirectToAction("GetAllSize");
            }
            else return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var Size = (await sizeServices.GetAllSize()).FirstOrDefault(c => c.Id == id);
            return View(Size);
        }
        public async Task<IActionResult> DeleteAsync(Size Size)
        {
           if (await sizeServices.DeleteSize(Size.Id) == true)
            {
                return RedirectToAction("GetAllSize");
            }
            else return BadRequest();
        }
        public IActionResult CreateAsync()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(string ten, decimal cm)
        {
            if (await sizeServices.AddSize(ten,cm) == true)
            {
                return RedirectToAction("GetAllsize");
            }
            else return BadRequest();
        }

    }
}
