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
    public class BillDetailsController : ControllerBase
    {
        private readonly IAllRepo<BillDetail> BillRepo;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<BillDetail> BillDetails;
        private readonly IAllRepo<ProductDetail> ProductRepo;
        DbSet<ProductDetail> ProductDetails;

        public BillDetailsController()
        {
            BillDetails = dbContextModel.BillDetails;
            ProductDetails = dbContextModel.ProductDetails;
            BillRepo = new AllRepo<BillDetail>(dbContextModel, BillDetails); ;
            ProductRepo = new AllRepo<ProductDetail>(dbContextModel, ProductDetails);
        }
        // GET: api/<BillDetailsController>
        [HttpGet]
        public IEnumerable<BillDetail> Get()
        {
            return BillRepo.GetAll().ToList();
        }

        // GET api/<BillDetailsController>/5
        [HttpGet("{id}")]
        public IEnumerable<BillDetail> Get(Guid id)
        {
            return BillRepo.GetAll().Where(c=>c.IdBill==id).ToList();
        }

        // POST api/<BillDetailsController>
        [HttpPost]
        public string Post(Guid idBill, Guid idProduct, int sl, int trangthai)
        {
            var b = BillRepo.GetAll().FirstOrDefault(c => c.IdBill == idBill && c.IdProductDetail == idProduct); 
            var c = ProductRepo.GetAll().FirstOrDefault(a => a.IdProduct == a.IdProduct);
            if (b != null)
            {
                b.SoLuong = b.SoLuong + sl;
                if(b.SoLuong >c.SoLuongTon)
                {
                    return "khum du so luong";
                }
                if(BillRepo.EditItem(b))
                return "san pham nay da co tron bill va da duoc cap nhap";
                return "khong thanh cong";

            }
            var d = new BillDetail() {Id = Guid.NewGuid(), IdBill = idBill, IdProductDetail = idProduct, DonGia = c.GiaBan, SoLuong = sl, TrangThai = trangthai };
            if (BillRepo.AddItem(d)) return "Them thanh cong";
            return "them khong thanh cong";
        }

        // PUT api/<BillDetailsController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, Guid idBill, Guid idProduct, int sl, int trangthai)
        {
            var c = ProductRepo.GetAll().FirstOrDefault(a => a.IdProduct == a.IdProduct);
            var a = new BillDetail() { Id = id, IdBill = idBill, IdProductDetail = idProduct, DonGia = (decimal)c.GiaBan, SoLuong = sl, TrangThai = trangthai };
            return BillRepo.EditItem(a);
        }

        // DELETE api/<BillDetailsController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            return BillRepo.RemoveItem(BillRepo.GetAll().FirstOrDefault(c => c.Id == id));
        }
    }
}
