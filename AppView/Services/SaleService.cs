using AppView.IServices;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using System.Drawing;

namespace AppView.Services
{
    public class SaleService : ISaleService
    {
        public async Task<bool> CreateSale(Sale p)
        {
            string apiUrl = $"https://localhost:7280/api/Sale?ma={p.Ma}&ten={p.Ten}&ngaybatdau={p.NgayBatDau}&ngayketthuc={p.NgayKetThuc}&LoaiHinhKm={p.LoaiHinhKm}&mota={p.MoTa}&mucgiam={p.MucGiam}&trangthai={p.TrangThai}";
            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync(apiUrl, null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            else return false;
        }

        public async Task<bool> DeleteSale(Guid id)
        {
            string apiUrl = $"https://localhost:7280/api/Sale/{id}";
            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else return false;
        }

        public async Task<bool> EditSale(Sale p)
        {
            string apiUrl = $"https://localhost:7280/api/Sale/{p.Id}?ma={p.Ma}&ten={p.Ten}&ngaybatdau={p.NgayBatDau}&ngayketthuc={p.NgayKetThuc}&LoaiHinhKm={p.LoaiHinhKm}&mota={p.MoTa}&mucgiam={p.MucGiam}&trangthai={p.TrangThai}";
            var httpClient = new HttpClient();

            var content = new StringContent(string.Empty);

            var response = await httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            else return false;
        }

        public async Task<List<Sale>> GetAllSale()
        {
            string apiUrl = "https://localhost:7280/api/Sale";
            var httpClient = new HttpClient(); // tạo ra để callApi
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var sales = JsonConvert.DeserializeObject<List<Sale>>(apiData);
            return sales;
        }
    }
}
