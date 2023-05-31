using AppData.IRepositories;
using AppData.Repositories;
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
        public ProductController()
        {
            product = context.Products;
            AllRepo<Product> all = new AllRepo<Product>(context, product);
            repos = all;
        }
        
        public async Task<IActionResult> GetAllSanPham()
        {
            string apiUrl = "https://localhost:7280/api/Product";
            var httpClient = new HttpClient(); // tạo ra để callApi
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var sanphams = JsonConvert.DeserializeObject<List<Product>>(apiData);
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
        public async Task<IActionResult> Create(string ma, string ten, int trangthai)
        {
            string apiUrl = $"https://localhost:7280/api/Product?ma={ma}&ten={ten}&trangthai={trangthai}";
            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync(apiUrl, null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllSanPham");
            }

            return View();
        }


        public async Task<IActionResult> DeleteSP(Guid id)
        {
            string apiUrl = $"https://localhost:7280/api/Product/{id}";
            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllSanPham");
            }

            return View();
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
        public async Task<IActionResult> EditSp(Guid id, string ma, string ten, int trangthai)
        {
            string apiUrl = $"https://localhost:7280/api/Product/{id}?ma={ma}&ten={ten}&trangthai={trangthai}";
            var httpClient = new HttpClient();

            var content = new StringContent(string.Empty);

            var response = await httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllSanPham");
            }

            return BadRequest();
        }



    }
}
