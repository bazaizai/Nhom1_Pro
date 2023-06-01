using Nhom1_Pro.Models;

namespace AppView.IServices
{
    public interface IMaterialServices
    {
        public Task<List<Material>> GetAllMaterial();
        public Task<bool> EditMaterial(Guid id, string ten, int trangthai);
        public Task<bool> DeleteMaterial(Guid id);
        public Task<bool> AddMaterial(string ten);
    }
}
