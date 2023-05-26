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
    public class CartDetailsController : ControllerBase
    {
        private readonly IAllRepo<CartDetail> allRepo;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<CartDetail> CartDetails;

        public CartDetailsController()
        {
            CartDetails = dbContextModel.CartDetails;
            AllRepo<CartDetail> all = new AllRepo<CartDetail>(dbContextModel, CartDetails);
            allRepo = all;
        }
        // GET: api/<CartDetailsController>
        [HttpGet]
        public IEnumerable<CartDetail> Get()
        {
            return allRepo.GetAll().ToList();
        }

        // GET api/<CartDetailsController>/5
        [HttpGet("{id}")]
        public IEnumerable<CartDetail> Get(Guid id)
        {
            return allRepo.GetAll().Where(c => c.UserID == id).ToList();
        }

        // POST api/<CartDetailsController>
        [HttpPost]
        public bool Post(Guid idUser, Guid idProduct, int sl, int dongia, int trangthai)
        {
            var a = new CartDetail() {  Id = Guid.NewGuid(), UserID = idUser, DetailProductID = idProduct, Dongia = dongia, Soluong = sl, TrangThai = trangthai };
            return allRepo.AddItem(a);
        }

        // PUT api/<CartDetailsController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, Guid idUser, Guid idProduct, int sl, int dongia, int trangthai)
        {
            var a = new CartDetail() { Id = id, UserID = idUser, DetailProductID = idProduct, Dongia = dongia, Soluong = sl, TrangThai = trangthai };
            return allRepo.AddItem(a);
        }

        // DELETE api/<CartDetailsController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            return allRepo.RemoveItem(allRepo.GetAll().FirstOrDefault(c => c.Id == id));
        }
    }
}
