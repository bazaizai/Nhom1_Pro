using Nhom1_Pro.Models;

namespace AppView.IServices
{
    public interface ISaleService
    {
        public Task<List<Sale>> GetAllSale();
        public Task<bool> CreateSale(Sale p);
        public Task<bool> EditSale(Sale p);
        public Task<bool> DeleteSale(Guid id);
    }
}
