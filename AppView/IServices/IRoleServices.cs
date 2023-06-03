using Nhom1_Pro.Models;

namespace AppView.IServices
{
    public interface IRoleServices
    {
        public Task<List<Role>> GetAllRole();
        public Task<bool> AddRole(string ten, int trangthai);
        public Task<bool> AddRoleGuest(Guid id,string ten, int trangthai);
        public Task<bool> GetByID(Guid id);
        public Task<bool> Edit(Guid id, string ten, int trangthai);
        public Task<bool> DeleteRole(Guid id);
    }
}
