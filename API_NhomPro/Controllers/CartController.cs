using AppData.IRepositories;
using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom1_Pro.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IAllRepo<Cart> repos;
        private readonly IAllRepo<User> Userrepos;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<Cart> Carts;
        DbSet<User> Users;
        public CartController()
        {
            Carts = dbContextModel.Carts;
            AllRepo<Cart> all = new AllRepo<Cart>(dbContextModel, Carts);
            AllRepo<User> all1 = new AllRepo<User>(dbContextModel, Users);
            repos = all;
            Userrepos = all1;
        }
        // GET: api/<CartController>
        [HttpGet]
        public IEnumerable<Cart> Get()
        {
            return repos.GetAll();
        }

        // GET api/<CartController>/5
        [HttpGet("name")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CartController>
        [HttpPost("Create-Cart")]
        public bool CreateCart(Guid Userid, string mota)
        {
            Cart cart = new Cart();
            cart.UserID = Userid;
            cart.Mota = mota;
            cart.TrangThai = 0;
            return repos.AddItem(cart);
        }

        // PUT api/<CartController>/5
        [HttpPut("Edit-Cart")]
        public bool EditCart(Guid Userid, string mota, int trangthai)
        {
            var cart = repos.GetAll().First(p => p.UserID == Userid);
            cart.Mota = mota;
            cart.TrangThai = trangthai;
            return repos.EditItem(cart);
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
