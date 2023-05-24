using AppData.IRepositories;
using AppData.Repositories;
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
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<Color> Colors;
        public ColorController()
        {
            Colors = dbContextModel.Colors;
            AllRepo<Color> all = new AllRepo<Color>(dbContextModel, Colors);
            allRepo = all;
        }
        public async Task<IActionResult> GetAllColorAsync(string search)
        {
            string apiUrl = "https://localhost:7280/api/Color/GetAllColor";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var Color = JsonConvert.DeserializeObject<List<Color>>(apiData);
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
            string apiUrl = $"https://localhost:7280/api/Color/EditColor?id={id}&ten={ten}&trangthai={trangthai}";
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsync(apiUrl, null);
            return RedirectToAction("GetAllColor");
        }
        [HttpGet]
        public IActionResult DeleteAsync(Guid id)
        {
            var Color = allRepo.GetAll().FirstOrDefault(c => c.Id == id);
            return View(Color);
        }
        public async Task<IActionResult> DeleteAsync(Color color)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Color/DeleteColor?id={color.Id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return RedirectToAction("GetAllColor");
        }
        public IActionResult CreateAsync()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(string ten)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Color/createColor?ten={ten}";
            var response = await httpClient.PostAsync(apiUrl, null);
            return RedirectToAction("GetAllColor");
        }

    }
}
