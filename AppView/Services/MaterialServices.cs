using AppView.IServices;
using Newtonsoft.Json;
using Nhom1_Pro.Models;


namespace AppView.Services
{
    public class MaterialServices : IMaterialServices
    {
        public async Task<bool> AddMaterial(string ten)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Material/createMaterial?ten={ten}";
            var response = await httpClient.PostAsync(apiUrl, null);
            return true;
        }

        public async Task<bool> DeleteMaterial(Guid id)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Material/DeleteMaterial?id={id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return true;
        }

        public async Task<bool> EditMaterial(Guid id, string ten, int trangthai)
        {
            string apiUrl = $"https://localhost:7280/api/Material/EditMaterial?id={id}&ten={ten}&trangthai={trangthai}";
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsync(apiUrl, null);
            return true;
        }

        public async Task<List<Material>> GetAllMaterial()
        {
            string apiUrl = "https://localhost:7280/api/Material/GetAllMaterial";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API request failed with status code {response.StatusCode}");
            }

            string apiData = await response.Content.ReadAsStringAsync();
            var Materials = JsonConvert.DeserializeObject<List<Material>>(apiData);
            return Materials;
        }


    }
}
