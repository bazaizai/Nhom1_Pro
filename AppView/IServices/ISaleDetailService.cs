using Nhom1_Pro.Models;

namespace AppView.IServices
{
    public interface ISaleDetailService
    {
        public Task<List<SaleDetail>> GetAllDetaiSale();
        public Task<bool> CreateDetaiSale(SaleDetail p);
        public Task<bool> EditDetaiSale(SaleDetail p);
        public Task<bool> DeleteDetaiSale(Guid id);
    }
}
