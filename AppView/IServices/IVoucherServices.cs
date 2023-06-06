using Nhom1_Pro.Models;

namespace AppView.IServices
{
    public interface IVoucherServices
    {
        public  Task<List<Voucher>> GetAllAsync();
        public  Task<Voucher> GetAllAsync(string item);
        public Task<bool> AddItemAsync(Voucher item);
        public Task<bool> RemoveItem(Voucher item);
        public Task<bool> EditItem(Voucher item);
    }
}
