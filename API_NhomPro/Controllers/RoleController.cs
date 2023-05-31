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
    public class RoleController : ControllerBase
    {
        private readonly IAllRepo<Role> repos;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<Role> Roles;
        public RoleController()
        {
            Roles = dbContextModel.Roles;
            AllRepo<Role> all = new AllRepo<Role>(dbContextModel, Roles);
            repos = all;
        }
        // GET: api/<RoleController>
        [HttpGet]
        public IEnumerable<Role> Get()
        {
            return repos.GetAll();
        }

        // GET api/<RoleController>/5
        [HttpGet("name")]
        public IEnumerable<Role> Get(string name)
        {
            return repos.GetAll().Where(p => p.Ten.ToLower().Contains(name.ToLower()));
        }

        // POST api/<RoleController>
        [HttpPost("Create-Role")]
        public bool CreateRole(string ten, int trangthai)
        {
            string ma;
            if (repos.GetAll().Count() == 0)
            {
                ma = "Role1";
            }
            else ma = "Role" + repos.GetAll().Max(c => Convert.ToInt32(c.Ma.Substring(4, c.Ma.Length - 4)) + 1);
            Role role = new Role();
            role.Id = Guid.NewGuid();
            role.Ma = ma;
            role.Ten = ten;
            role.TrangThai = trangthai;
            return repos.AddItem(role);
        }

        // PUT api/<RoleController>/5
        [HttpPut("Edit-Role")]
        public bool EditRole(Guid id, string ten, int trangthai)
        {
            var role = repos.GetAll().First(p => p.Id == id);
            role.Ten = ten;
            role.TrangThai = trangthai;
            return repos.EditItem(role);
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("Delete-Role")]
        public bool DeleteRole(Guid id)
        {
            var role = repos.GetAll().First(p => p.Id == id);
            return repos.RemoveItem(role);
        }
    }
}
