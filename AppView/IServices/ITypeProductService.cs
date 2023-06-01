using Nhom1_Pro.Models;

namespace AppView.IServices
{
    public interface ITypeProductService
    {
        public Task<bool> CreateTypeProductAsync(TypeProduct obj);
        public Task<bool> UpdateTypeProductAsync(TypeProduct obj);
        public Task<bool> DeleteTypeProductAsync(Guid id);
        public Task<List<TypeProduct>> GetAllTypeProductsAsync();
        public TypeProduct GetTypeProductById(Guid id);
        public List<TypeProduct> GetTypeProductsByName(string name);
    }
}
