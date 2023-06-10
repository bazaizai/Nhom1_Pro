using Nhom1_Pro.Models;

namespace AppView.IServices
{
    public interface IUserServices
    {
        public Task<List<User>> GetAllUser();
        public Task<bool> AddUser(User user);
        public Task<bool> GetByID(Guid id);
        public Task<User> GetByLogin(string taikhoan,string matkhau);
        public Task<bool> GetByName(string name);
        public Task<bool> Edit(User user);
        public Task<bool> DeleteUser(Guid id);
    }
}
