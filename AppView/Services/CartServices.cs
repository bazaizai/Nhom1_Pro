using AppView.IServices;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using System.Drawing;

namespace AppView.Services
{
    public class CartServices : ICartServices
    {
        public async Task<bool> AddCart(Guid idUser, string mota)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Cart/Create-Cart?Userid={idUser}&mota={mota}";

            try
            {
                var response = await httpClient.PostAsync(apiUrl, null);
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây
                Console.WriteLine($"Lỗi khi thêm giỏ hàng: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCart(Guid id)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Cart/Delete-Cart?id={id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return true;
        }

        public async Task<bool> Edit(Guid idUser, string mota)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Cart/Edit-Cart?Userid= {idUser} &mota= {mota}";
            var response = await httpClient.PutAsync(apiUrl, null);
            return true;
        }

        public async Task<List<Cart>> GetAllCart()
        {
            string apiUrl = "https://localhost:7280/api/Cart";
            var httpClient = new HttpClient(); // tao ra để callApi
            var response = await httpClient.GetAsync(apiUrl);
            // Lấy dữ liệu Json trả về từ Api được call dang string
            string apiData = await response.Content.ReadAsStringAsync();
            // Đọc từ string Json vừa thu được sang List<T>
            var cart = JsonConvert.DeserializeObject<List<Cart>>(apiData);
            return cart;
        }

        public Task<bool> GetByID(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
