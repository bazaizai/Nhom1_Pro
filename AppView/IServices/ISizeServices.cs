using Nhom1_Pro.Models;

namespace AppView.IServices
{
    public interface ISizeServices
    {
        public Task<List<Size>> GetAllSize();
        public Task<bool> EditSize(Guid id, string ten, int trangthai, decimal cm);
        public Task<bool> DeleteSize(Guid id);
        public Task<bool> AddSize(string ten, decimal cm);
    }
}
