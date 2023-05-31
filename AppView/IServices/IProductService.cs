using Nhom1_Pro.Models;

namespace AppView.IServices
{
    public interface Isalesevice
    {
        public Task<List<Product>> GetAllSanPham();
        public Task<bool> CreateSanPham( Product p);
        public Task<bool> EditSanPham( Product p);
        public Task<bool> DeleteSanPham(Guid id);
       
    }
}
