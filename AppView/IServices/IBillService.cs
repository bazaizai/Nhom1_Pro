using Nhom1_Pro.Models;

namespace AppView.IServices
{
    public interface IBillService
    {
        public Task<bool> CreateBillAsync(Bill obj);
        public Task<bool> UpdateBillAsync(Bill obj);
        public Task<bool> DeleteBillAsync(Guid id);
        public Task<List<Bill>> GetAllBillsAsync();
        public Bill GetBillById(Guid id);
        public List<Bill> GetBillsByMa(string name);
        public Task<List<Bill>> GetBillsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
