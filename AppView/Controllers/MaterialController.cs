using AppData.IRepositories;
using AppData.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nhom1_Pro.Models;

namespace AppView.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IAllRepo<Material> allRepo;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<Material> Materials;
        public MaterialController()
        {
            Materials = dbContextModel.Materials;
            AllRepo<Material> all = new AllRepo<Material>(dbContextModel, Materials);
            allRepo = all;
        }
        public async Task<IActionResult> GetAllMaterialAsync(string search)
        {
            string apiUrl = "https://localhost:7280/api/Material/GetAllMaterial";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var Material = JsonConvert.DeserializeObject<List<Material>>(apiData);
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
        public IActionResult DetailsAsync(Guid id)
        {
            var Material = allRepo.GetAll().FirstOrDefault(c => c.Id == id);
            return View(Material);
        }
        [HttpGet]
        public IActionResult EditAsync(Guid id)
        {
            var Material = allRepo.GetAll().FirstOrDefault(c => c.Id == id);
            return View(Material);
        }
        public async Task<IActionResult> EditAsync(Guid id, string ten, int trangthai)
        {
            string apiUrl = $"https://localhost:7280/api/Material/EditMaterial?id={id}&ten={ten}&trangthai={trangthai}";
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsync(apiUrl, null);
            return RedirectToAction("GetAllMaterial");
        }
        [HttpGet]
        public IActionResult DeleteAsync(Guid id)
        {
            var Material = allRepo.GetAll().FirstOrDefault(c => c.Id == id);
            return View(Material);
        }
        public async Task<IActionResult> DeleteAsync(Material Material)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Material/DeleteMaterial?id={Material.Id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return RedirectToAction("GetAllMaterial");
        }
        public IActionResult CreateAsync()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(string ten)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Material/createMaterial?ten={ten}";
            var response = await httpClient.PostAsync(apiUrl, null);
            return RedirectToAction("GetAllMaterial");
        }
    }
}
