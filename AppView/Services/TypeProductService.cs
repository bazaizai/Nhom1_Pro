using AppView.IServices;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using System;

namespace AppView.Services
{
    public class TypeProductService : ITypeProductService
    {
        public async Task<bool> CreateTypeProductAsync(TypeProduct obj)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiUrl = $"https://localhost:7280/api/TypeProduct?ten={obj.Ten}&ma={obj.Ma}& trangThai={obj.TrangThai}";
                var response = await httpClient.PostAsync(apiUrl, null);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteTypeProductAsync(Guid id)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiUrl = $"https://localhost:7280/api/TypeProduct/Delete-TypeProduct?id={id}";
                var response = await httpClient.DeleteAsync(apiUrl);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<TypeProduct>> GetAllTypeProductsAsync()
        {
            string apiUrl = "https://localhost:7280/api/TypeProduct";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);

            string apiData = await response.Content.ReadAsStringAsync();

            var typeProducts = JsonConvert.DeserializeObject<List<TypeProduct>>(apiData);
            return typeProducts;
        }

        public TypeProduct GetTypeProductById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<TypeProduct> GetTypeProductsByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateTypeProductAsync(TypeProduct obj)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiUrl = $"https://localhost:7280/api/TypeProduct/{obj.Id}?ten={obj.Ten}&ma={obj.Ma}& trangThai={obj.TrangThai}";
                var response = await httpClient.PutAsync(apiUrl, null);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
