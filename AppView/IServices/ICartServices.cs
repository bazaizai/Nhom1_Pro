using Nhom1_Pro.Models;

namespace AppView.IServices
{
    public interface ICartServices
    {
        public Task<List<Cart>> GetAllCart();
        public Task<bool> AddCart(Guid idUser, string mota);
        public Task<bool> GetByID(Guid id);
        public Task<bool> Edit(Guid idUser, string mota);
        public Task<bool> DeleteCart(Guid id);
    }
}
