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
    public class MaterialController : Controller
    {

        private readonly IMaterialServices MaterialServices;
        public MaterialController()
        {
            MaterialServices = new MaterialServices();
        }
        public async Task<IActionResult> GetAllMaterialAsync(string search)
        {
            var Material = await MaterialServices.GetAllMaterial();
            if (search == null)
            {
                return View(Material);
            }
            else
            {
                var Materials = Material.Where(c => c.Ten.ToUpper().Contains(search.ToUpper())).ToList();
                return View(Materials);
            }

        }
        [HttpGet]
        public async Task<IActionResult> DetailsAsync(Guid id)
        {
            var Material = (await MaterialServices.GetAllMaterial()).FirstOrDefault(c => c.Id == id);

            return View(Material);
        }
        [HttpGet]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var Material = (await MaterialServices.GetAllMaterial()).FirstOrDefault(c => c.Id == id);
            return View(Material);
        }
        public async Task<IActionResult> EditAsync(Guid id, string ten, int trangthai)
        {
            if (await MaterialServices.EditMaterial(id, ten, trangthai) == true)
            {
                return RedirectToAction("GetAllMaterial");
            }
            else return BadRequest();

        }
        [HttpGet]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var Material = (await MaterialServices.GetAllMaterial()).FirstOrDefault(c => c.Id == id);
            return View(Material);
        }
        public async Task<IActionResult> DeleteAsync(Material Material)
        {
            if (await MaterialServices.DeleteMaterial(Material.Id) == true)
            {
                return RedirectToAction("GetAllMaterial");
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
            if (await MaterialServices.AddMaterial(ten) == true)
            {
                return RedirectToAction("GetAllMaterial");
            }
            else return BadRequest();
        }

    }
}
