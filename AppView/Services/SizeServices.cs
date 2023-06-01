using AppView.IServices;
using Newtonsoft.Json;
using Nhom1_Pro.Models;

namespace AppView.Services
{
    public class SizeServices : ISizeServices
    {
        public async Task<bool> AddSize(string ten, decimal cm)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Size/createSize?tenSize={ten}&CM={cm}";
            var response = await httpClient.PostAsync(apiUrl, null);
            return true;
        }

        public async Task<bool> DeleteSize(Guid id)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Size/DeleteSize?id={id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return true;
        }

        public async Task<bool> EditSize(Guid id, string ten, int trangthai, decimal cm)
        {
            string apiUrl = $"https://localhost:7280/api/Size/EditSize?id={id}&ten={ten}&CM={cm}&trangthai={trangthai}";
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsync(apiUrl, null);
            return true;
        }

        public async Task<List<Size>> GetAllSize()
        {
            string apiUrl = "https://localhost:7280/api/Size/GetAllSize";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API request failed with status code {response.StatusCode}");
            }

            string apiData = await response.Content.ReadAsStringAsync();
            var Sizes = JsonConvert.DeserializeObject<List<Size>>(apiData);
            return Sizes;
        }
    }
}
