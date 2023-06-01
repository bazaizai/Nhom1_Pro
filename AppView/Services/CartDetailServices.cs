using AppView.IServices;
using Newtonsoft.Json;
using Nhom1_Pro.Models;

namespace AppView.Services
{
    public class CartDetailServices:ICartDetailServices
    {
        public async Task<bool> AddItemAsync(CartDetail item)
        {
            string apiUrl = $"https://localhost:7280/api/CartDetails?idUser={item.UserID}&idProduct={item.DetailProductID}&sl={item.Soluong}&dongia={item.Dongia}&trangthai={item.TrangThai}";
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(apiUrl, null);
            return true;
        }

        public async Task<bool> EditItem(CartDetail item)
        {
            string apiUrl = $"https://localhost:7280/api/CartDetails/{item.Id}?idUser={item.UserID}&idProduct={item.DetailProductID}&sl={item.Soluong}&trangthai={item.TrangThai}";
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(apiUrl, null);
            return true;
        }

        public async Task<List<CartDetail>> GetAllAsync()
        {
            string apiUrl = "https://localhost:7280/api/CartDetails";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(apiUrl);
            var apidata = await response.Content.ReadAsStringAsync();
            List<CartDetail> lstBill = JsonConvert.DeserializeObject<List<CartDetail>>(apidata);
            return lstBill;
        }

        public async Task<List<CartDetail>> GetById(Guid id)
        {
            string apiUrl = $"https://localhost:7280/api/CartDetails/{id}";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(apiUrl);
            var apidata = await response.Content.ReadAsStringAsync();
            List<CartDetail> lstBill = JsonConvert.DeserializeObject<List<CartDetail>>(apidata);
            return lstBill;
        }

        public async Task<bool> RemoveItem(CartDetail item)
        {
            string apiUrl = $"https://localhost:7280/api/CartDetails/{item.Id}";
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(apiUrl, null);
            return true;
        }
    }
}
