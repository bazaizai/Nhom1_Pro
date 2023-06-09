
using AppData.Models;
using Nhom1_Pro.Models;

namespace AppView.IServices
{
    public interface ICartDetailServices
    {
        public Task<List<CartViewModel>> GetAllAsync();
        public Task<List<CartDetail>> GetById(Guid id);
        public Task<bool> AddItemAsync(CartDetail item);
        public Task<bool> RemoveItem(Guid id);
        public Task<bool> EditItem(CartDetail item);
        
    }
}
