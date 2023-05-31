using AppView.IServices;
using Newtonsoft.Json;
using Nhom1_Pro.Models;

namespace AppView.Services
{
    public class SaleDetailService : ISaleDetailService
    {
        public async Task<bool> CreateDetaiSale(SaleDetail p)
        {
            string apiUrl = $"https://localhost:7280/api/SaleDetail?mota={p.MoTa}&trangthai={p.TrangThai}&IdSale={p.IdSale}&IdChiTietSp={p.IdChiTietSp}";
            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync(apiUrl, null);


            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else return false;
        }

        public async Task<bool> DeleteDetaiSale(Guid id)
        {
            string apiUrl = $"https://localhost:7280/api/SaleDetail/{id}";
            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else return false;

        }

        public async Task<bool> EditDetaiSale(SaleDetail p)
        {
            string apiUrl = $"https://localhost:7280/api/SaleDetail/{p.Id}?mota={p.MoTa}&trangthai={p.TrangThai}&IdSale={p.IdSale}&IdChiTietSp={p.IdChiTietSp}";
            var httpClient = new HttpClient();

            var content = new StringContent(string.Empty);

            var response = await httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            else return false;
        }

        public async Task<List<SaleDetail>> GetAllDetaiSale()
        {
            string apiUrl = "https://localhost:7280/api/SaleDetail";
            var httpClient = new HttpClient(); // tạo ra để callApi
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var SaleDetails = JsonConvert.DeserializeObject<List<SaleDetail>>(apiData);
            return SaleDetails;
        }
    }
}
