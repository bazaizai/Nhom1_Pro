using AppView.IServices;
using Microsoft.AspNetCore.Rewrite;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using System.Drawing;
using System.Net;
using System.Xml.Linq;

namespace AppView.Services
{
    public class UserServices : IUserServices
    {
        public async Task<bool> AddUser(User user)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/User/Create-User?id={user.Id}&idRole={user.IdRole}&ten={user.Ten}&gioitinh={user.GioiTinh}&ngaysinh={user.NgaySinh}&diachi={user.DiaChi}&sdt={user.Sdt}&matkhau={user.MatKhau}&email={user.Email}&taikhoan={user.TaiKhoan}&trangthai={user.TrangThai}";
            try
            {
                var response = await httpClient.PostAsync(apiUrl, null);
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }    
                return false;
              
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây
                Console.WriteLine($"Lỗi khi thêm người dùng: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/User/Delete-User?id={id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return true;
        }

        public async Task<bool> Edit(User user)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/User/Edit-User?id={user.Id}&ten={user.Ten}&GioiTinh={user.GioiTinh}&NgaySinh={user.NgaySinh}&diachi={user.DiaChi}&sdt={user.Sdt}&matkhau={user.MatKhau}&email={user.Email}&taikhoan={user.TaiKhoan}&trangthai={user.TrangThai}";
            var response = await httpClient.PutAsync(apiUrl, null);
            return true;
        }

        public async Task<List<User>> GetAllUser()
        {
            string apiUrl = "https://localhost:7280/api/User"; 
            var httpClient = new HttpClient(); // tạo ra để callApi
            var response = await httpClient.GetAsync(apiUrl);
            // Lấy dữ liệu Json trả về từ Api được call dạng string
            string apiData = await response.Content.ReadAsStringAsync();
            // Đọc từ string Json vừa thu  được sang List<T>
            var users = JsonConvert.DeserializeObject<List<User>>(apiData);
            return users;
        }

        public Task<bool> GetByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByLogin(string taikhoan, string matkhau)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/User/GetUserByName?taikhoan={taikhoan}&matkhau={matkhau}";
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<User>(apiData);
            return users ;
        }

        public async Task<bool> GetByName(string name)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/User/name?name={name}";
            var response = await httpClient.PutAsync(apiUrl, null);
            return true;
        }
    }
}
