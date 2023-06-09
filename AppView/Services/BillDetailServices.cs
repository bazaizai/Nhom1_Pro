using AppData.Models;
using AppView.IServices;
using Newtonsoft.Json;
using Nhom1_Pro.Models;

namespace AppView.Services
{
    public class BillDetailServices : IBillDetailServices
    {
        public async Task<bool> AddItemAsync(BillDetail item)
        {
            string apiUrl = $"https://localhost:7280/api/BillDetails?idBill={item.IdBill}&idProduct={item.IdProductDetail}&sl={item.SoLuong}&trangthai={item.TrangThai}";
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(apiUrl,null);
            return true;
        }

        public async Task<bool> EditItem(BillDetail item)
        {
            string apiUrl = $"https://localhost:7280/api/BillDetails/{item.Id}?idBill={item.IdBill}&idProduct={item.IdProductDetail}&sl={item.SoLuong}&trangthai={item.TrangThai}";
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(apiUrl, null);
            return true;
        }

        public async Task<List<BillDetail>> GetAllAsync()
        {
            string apiUrl = "https://localhost:7280/api/BillDetails";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(apiUrl);
            var apidata = await response.Content.ReadAsStringAsync();
            List<BillDetail> lstBill = JsonConvert.DeserializeObject<List<BillDetail>>(apidata);
            return lstBill;
        }

        public async Task<List<BillDetailView>> GetByBill(Guid id)
        {
            string apiUrl = $" https://localhost:7280/api/BillDetails/idBill?id={id}";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(apiUrl);
            var apidata = await response.Content.ReadAsStringAsync();
            List<BillDetailView> lstBill = JsonConvert.DeserializeObject<List<BillDetailView>>(apidata);
            return lstBill;
        }

        public async Task<List<BillDetail>> GetById(Guid id)
        {
            string apiUrl = $"https://localhost:7280/api/BillDetails/{id}";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(apiUrl);
            var apidata = await response.Content.ReadAsStringAsync();
            List<BillDetail> lstBill = JsonConvert.DeserializeObject<List<BillDetail>>(apidata);
            return lstBill;
        }

        public async Task<bool> RemoveItem(BillDetail item)
        {
            string apiUrl = $"https://localhost:7280/api/BillDetails/{item.Id}";
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(apiUrl, null);
            return true;
        }
    }
}
