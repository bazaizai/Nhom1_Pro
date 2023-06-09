using AppData.Models;
using Nhom1_Pro.Models;

namespace AppView.IServices
{
    public interface IBillDetailServices
    {
        public Task<List<BillDetail>> GetAllAsync();
        public Task<List<BillDetail>> GetById(Guid id);
        public Task<List<BillDetailView>> GetByBill(Guid id);
        public Task<bool> AddItemAsync(BillDetail item);
        public Task<bool> RemoveItem(BillDetail item);
        public Task<bool> EditItem(BillDetail item);
    }
}
