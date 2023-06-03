using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Nhom1_Pro.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAllRepo<User> repos;
        private readonly IAllRepo<Cart> Cartrepos;
        private readonly IAllRepo<Role> Rolerepos;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<User> Users;
        DbSet<Role> Roles;
        DbSet<Cart> Carts;
        public UserController()
        {
            Users = dbContextModel.Users;
            Carts = dbContextModel.Carts;
            Roles = dbContextModel.Roles;
            AllRepo<User> all = new AllRepo<User>(dbContextModel, Users);
            AllRepo<Cart> allCart = new AllRepo<Cart>(dbContextModel, Carts);
            AllRepo<Role> allRole = new AllRepo<Role>(dbContextModel, Roles);
            repos = all;
            Cartrepos = allCart;
            Rolerepos = allRole;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return repos.GetAll();
        }
        //[HttpGet("code")]
        //public ActionResult GetAllBienThe()
        //{
        //    var selectList = new
        //    {
        //        roles = new SelectList(Rolerepos.GetAll().Where(cl => cl.TrangThai == 0).ToList(), "Id", "Ten"),
        //    };
        //    return Ok(selectList);
        //}
        // GET api/<UserController>/5
        [HttpGet("name")]
        public IEnumerable<User> Get(string name)
        {
            return repos.GetAll().Where(p => p.Ten.ToLower().Contains(name.ToLower()));
        }

        // POST api/<UserController>
        [HttpPost("Create-User")]
        public bool CreateUser(Guid id, Guid idRole, string ten, int gioitinh, DateTime ngaysinh, string diachi, string sdt, string matkhau, string email, string taikhoan, int trangthai)
        {
            string ma;
            if (repos.GetAll().Count() == 0)
            {
                ma = "User1";
            }
            else ma = "User" + repos.GetAll().Max(c => Convert.ToInt32(c.Ma.Substring(4, c.Ma.Length - 4)) + 1);
            User user = new User();
            user.Id = id;
            user.Ten = ten;
            user.Ma = ma;
            user.GioiTinh = gioitinh;
            user.NgaySinh = ngaysinh;
            user.DiaChi = diachi;
            user.Sdt = sdt;
            user.Email = email;
            user.MatKhau = matkhau;
            user.TaiKhoan = taikhoan;
            user.TrangThai = trangthai;
            user.IdRole = idRole;
            return repos.AddItem(user);
            //Cart cart = new Cart();
            //cart.UserID = user.Id;
            ////cart.Mota = mota;
            //return Cartrepos.AddItem(cart);
        }

        // PUT api/<UserController>/5
        [HttpPut("Edit-User")]
        public bool EditUser(Guid id, string ten, int GioiTinh, DateTime NgaySinh, string diachi, string sdt, string matkhau, string email, string taikhoan, int trangthai)
        {
            var user = repos.GetAll().First(p => p.Id == id);
            user.Ten = ten;
            user.GioiTinh = GioiTinh;
            user.NgaySinh = NgaySinh;
            user.DiaChi = diachi;
            user.Sdt = sdt;
            user.Email = email;
            user.MatKhau = matkhau;
            user.TaiKhoan = taikhoan;
            user.TrangThai = trangthai;
            return repos.EditItem(user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("Delete-User")]
        public bool DeleteUser(Guid id)
        {
            var user = repos.GetAll().First(p => p.Id == id);
            return repos.RemoveItem(user);
        }
    }
}
