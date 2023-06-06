using AppData.Models;
using AppView.IServices;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using System.Net;
using System.Net.Http;
using System.Text;

namespace AppView.Services
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly HttpClient client;
        public ProductDetailService()
        {
            client = new HttpClient();
        }
        public async Task<HttpResponseMessage> AddItem(ProductDetailViewModel obj)
        {
            var apiUrl = "https://localhost:7280/api/SanPhamCT/Create-ProductDetail";
            return await client.PostAsJsonAsync(apiUrl, obj);
        }

        public async Task<IEnumerable<ProductDetailDTO>> GetAll()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7280/api/SanPhamCT/list-SanPhamCT");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<ProductDetailDTO>>();
            }
            return null;
        }

        public async Task<object> GetAllBienThe()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7280/api/SanPhamCT/list-BienThe");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<dynamic>(responseContent);
                return data;
            }
            return null;
        }

        public async Task<ProductDetailDTO> GetById(Guid id)
        {
            return (await GetAll()).FirstOrDefault(x => x.Id == id);
        }



        public async Task<List<ProductDetailDTO>> GetByName(string name)
        {
            return name != null ? (await GetAll()).Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList() : (await GetAll()).ToList();
        }

        public async Task<ProductDetailPutViewModel> GetProductUpdate(Guid id)
        {
            var response = await client.GetAsync($"https://localhost:7280/api/SanPhamCT/Get-ProductDetailPut?id={id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<ProductDetailPutViewModel>();
        }

        public async Task<bool> RemoveItem(Guid id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7280/api/SanPhamCT/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<bool>();
            }
            return false;
        }

        public async Task<bool> UpdateItem(ProductDetailPutViewModel item)
        {
            var url = "https://localhost:7280/api/SanPhamCT/Update-ProductDetail";
            HttpResponseMessage response = await client.PutAsJsonAsync(url,item);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<bool>();
        }

        public async Task UpdateSoLuong(Guid id, int soLuongCart)
        {
             await client.GetAsync($"https://localhost:7280/api/SanPhamCT/Update-soLuong?Id={id}&soLuongCart={soLuongCart}");
        }

    }
}
