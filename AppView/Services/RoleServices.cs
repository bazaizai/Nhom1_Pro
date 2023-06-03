using AppView.IServices;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using NuGet.Protocol;

namespace AppView.Services
{
    public class RoleServices : IRoleServices
    {

        public RoleServices()
        {

        }
        public async Task<bool> AddRole(string ten, int trangthai)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Role/Create-Role?ten={ten}&trangthai={trangthai}";
            var response = await httpClient.PostAsync(apiUrl, null);
            return true;
        }

        public async Task<bool> AddRoleGuest(Guid id, string ten, int trangthai)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Role/Create-Role-Guest?id={id}&ten={ten}&trangthai={trangthai}";
            var response = await httpClient.PostAsync(apiUrl, null);
            return true;
        }

        public async Task<bool> DeleteRole(Guid id)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Role/Delete-Role?id={id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return true;
        }

        public async Task<bool> Edit(Guid id, string ten, int trangthai)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Role/Edit-Role?id={id}&ten={ten}&trangthai={trangthai}";
            var response = await httpClient.PutAsync(apiUrl, null);
            return true;
        }

        public async Task<List<Role>> GetAllRole()
        {
            string apiUrl = "https://localhost:7280/api/Role";
            var httpClient = new HttpClient(); // tao ra để callApi
            var response = await httpClient.GetAsync(apiUrl);
            // Lấy dữ liệu Json trả về từ Api được call dang string
            string apiData = await response.Content.ReadAsStringAsync();
            // Đọc từ string Json vừa thu được sang List<T>
            var roles = JsonConvert.DeserializeObject<List<Role>>(apiData);
            return roles;

        }

        public Task<bool> GetByID(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
