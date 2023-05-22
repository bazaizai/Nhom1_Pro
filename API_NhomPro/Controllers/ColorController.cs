using AppData.IRepositories;
using AppData.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom1_Pro.Models;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IAllRepo<Color> allRepo;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<Color> Colors;
        public ColorController()
        {
            Colors = dbContextModel.Colors;
            AllRepo<Color> all = new AllRepo<Color>(dbContextModel, Colors);
            allRepo = all;
        }
        [HttpGet("Name")]
        public IEnumerable<Color> Get(string name)
        {
            return allRepo.GetAll().Where(c=>c.Ten == name).ToList();
        }
        [HttpPost("create")]
        public bool createColor(string ma, string ten, int trangthai)
        {
            var color = new Color();
            color.Ten = ten;color.Id = Guid.NewGuid(); color.Ma = ma;color.TrangThai = trangthai;
            return allRepo.AddItem(color);
        }
    }
}
