using AppView.IServices;
using Newtonsoft.Json;
using Nhom1_Pro.Models;


namespace AppView.Services
{
    public class ColorServices : IColorServices
    {
        public async Task<bool> AddColor(string ten)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Color/createColor?ten={ten}";
            var response = await httpClient.PostAsync(apiUrl, null);
            return true;
        }

        public async Task<bool> DeleteColor(Guid id)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Color/DeleteColor?id={id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return true;
        }

        public async Task<bool> EditColor(Guid id, string ten, int trangthai)
        {
            string apiUrl = $"https://localhost:7280/api/Color/EditColor?id={id}&ten={ten}&trangthai={trangthai}";
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsync(apiUrl, null);
            return true;
        }

        public async Task<List<Color>> GetAllColor()
        {
            string apiUrl = "https://localhost:7280/api/Color/GetAllColor";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API request failed with status code {response.StatusCode}");
            }

            string apiData = await response.Content.ReadAsStringAsync();
            var Colors = JsonConvert.DeserializeObject<List<Color>>(apiData);
            return Colors;
        }

   
    }
}
