using Microsoft.AspNetCore.Mvc;
using Nhom1_Pro.Models;

namespace AppView.IServices
{
    public interface IColorServices
    {
        public Task<List<Color>> GetAllColor();
        public Task<bool> EditColor(Guid id, string ten, int trangthai);
        public Task<bool> DeleteColor(Guid id);
        public Task<bool> AddColor(string ten);

    }
}
