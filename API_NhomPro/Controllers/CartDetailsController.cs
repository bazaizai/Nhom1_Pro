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
        private readonly IAllRepo<ProductDetail> ProductRepo;
        DbSet<ProductDetail> ProductDetails;
        public CartDetailsController()
        {
            CartDetails = dbContextModel.CartDetails;
            ProductDetails = dbContextModel.ProductDetails;
            allRepo = new AllRepo<CartDetail>(dbContextModel, CartDetails);
            ProductRepo = new AllRepo<ProductDetail>(dbContextModel, ProductDetails);
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
        public string Post(Guid idUser, Guid idProduct, int sl, int trangthai)
        {
            var b = allRepo.GetAll().FirstOrDefault(c => c.UserID == idUser && c.DetailProductID == idProduct);
            var c = ProductRepo.GetAll().FirstOrDefault(a => a.IdProduct == a.IdProduct);
            if (b != null)
            {
                b.Soluong = b.Soluong + sl;
                if (b.Soluong > c.SoLuongTon)
                {
                    return "khum du so luong";
                }
                if (allRepo.EditItem(b))
                    return "san pham nay da co tron bill va da duoc cap nhap";
                return "khong thanh cong";

            }
            var a = new CartDetail() {  Id = Guid.NewGuid(), UserID = idUser, DetailProductID = idProduct, Dongia = (decimal)c.GiaBan, Soluong = sl, TrangThai = trangthai };
            if (allRepo.AddItem(a)) return "Them thanh cong";
            return "them khong thanh cong";
        }

        // PUT api/<CartDetailsController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, Guid idUser, Guid idProduct, int sl, int trangthai)
        {
            var c = ProductRepo.GetAll().FirstOrDefault(a => a.IdProduct == a.IdProduct);
            var a = new CartDetail() { Id = id, UserID = idUser, DetailProductID = idProduct, Dongia = (decimal)c.GiaBan, Soluong = sl, TrangThai = trangthai };
            return allRepo.EditItem(a);
        }

        // DELETE api/<CartDetailsController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            return allRepo.RemoveItem(allRepo.GetAll().FirstOrDefault(c => c.Id == id));
        }
    }
}
