using AppData.IRepositories;
using AppData.Repositories;
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
        private readonly IAllRepo<Size> allRepo;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<Size> Sizes;
        public SizeController()
        {
            Sizes = dbContextModel.Sizes;
            AllRepo<Size> all = new AllRepo<Size>(dbContextModel, Sizes);
            allRepo = all;
        }
        public async Task<IActionResult> GetAllSizeAsync(string search)
        {
            string apiUrl = "https://localhost:7280/api/Size/GetAllSize";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var Size = JsonConvert.DeserializeObject<List<Size>>(apiData);
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
        public IActionResult DetailsAsync(Guid id)
        {
            var Size = allRepo.GetAll().FirstOrDefault(c => c.Id == id);
            return View(Size);
        }
        [HttpGet]
        public IActionResult EditAsync(Guid id)
        {
            var Size = allRepo.GetAll().FirstOrDefault(c => c.Id == id);
            return View(Size);
        }
        public async Task<IActionResult> EditAsync(Guid id, string ten, int trangthai, decimal cm)
        {
            string apiUrl = $"https://localhost:7280/api/Size/EditSize?id={id}&ten={ten}&CM={cm}&trangthai={trangthai}";
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsync(apiUrl, null);
            return RedirectToAction("GetAllSize");
        }
        [HttpGet]
        public IActionResult DeleteAsync(Guid id)
        {
            var Size = allRepo.GetAll().FirstOrDefault(c => c.Id == id);
            return View(Size);
        }
        public async Task<IActionResult> DeleteAsync(Size Size)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Size/DeleteSize?id={Size.Id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return RedirectToAction("GetAllSize");
        }
        public IActionResult CreateAsync()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(string ten, decimal cm)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Size/createSize?tenSize={ten}&CM={cm}";
            var response = await httpClient.PostAsync(apiUrl, null);
            return RedirectToAction("GetAllSize");
        }

    }
}
