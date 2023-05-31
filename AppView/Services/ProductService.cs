using AppView.IServices;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using System.Drawing;

namespace AppView.Services
{
    public class ProductService : Isalesevice
    {
        public async Task<bool> CreateSanPham(Product p)
        {
            string apiUrl = $"https://localhost:7280/api/Product?ma={p.Ma}&ten={p.Ten}&trangthai={p.TrangThai}";
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(apiUrl, null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
         else { return false; }
        }

        public async Task<bool> DeleteSanPham(Guid id)
        {
            string apiUrl = $"https://localhost:7280/api/Product/{id}";
            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else { return false; }

        }

        public async Task<bool> EditSanPham(Product p)
        {
            string apiUrl = $"https://localhost:7280/api/Product/{p.Id}?ma={p.Ma}&ten={p.Ten}&trangthai={p.TrangThai}";
            var httpClient = new HttpClient();

            var content = new StringContent(string.Empty);

            var response = await httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            else return false;
        }

        public async Task<List<Product>> GetAllSanPham()
        {
            string apiUrl = "https://localhost:7280/api/Product";
            var httpClient = new HttpClient(); // tạo ra để callApi
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var sanphams = JsonConvert.DeserializeObject<List<Product>>(apiData);
            return sanphams;
        }
    }
}
