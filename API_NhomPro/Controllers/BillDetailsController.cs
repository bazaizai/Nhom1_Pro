using AppData.IRepositories;
using AppData.Models;
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
        private readonly IAllRepo<ProductDetail> ProductDetailRepo;
        DbSet<ProductDetail> ProductDetails;
        private readonly IAllRepo<Product> ProductRepo;
        DbSet<Product> Products;

        public BillDetailsController()
        {
            BillDetails = dbContextModel.BillDetails;
            ProductDetails = dbContextModel.ProductDetails;
            BillRepo = new AllRepo<BillDetail>(dbContextModel, BillDetails); ;
            ProductDetailRepo = new AllRepo<ProductDetail>(dbContextModel, ProductDetails);
            Products = dbContextModel.Products;
            ProductRepo = new AllRepo<Product>(dbContextModel, Products);
        }
        // GET: api/<BillDetailsController>
        [HttpGet]
        public IEnumerable<BillDetail> Get()
        {
            return BillRepo.GetAll().ToList();
        }
        [HttpGet("idBill")]
        public IEnumerable<BillDetailView> GetByBill(Guid id)
        {
            var lst = (from a in BillRepo.GetAll()
                       join b in (from a1 in ProductDetailRepo.GetAll()
                                  join b1 in ProductRepo.GetAll() on a1.IdProduct equals b1.Id
                                  select new ProductDetailDTO()
                                  {
                                      Id = a1.Id,
                                      Name = b1.Ten
                                  }
                                  ).ToList() on a.IdProductDetail equals b.Id
                       select new BillDetailView()
                       {
                           Id = a.Id,
                           DonGia = a.DonGia,
                           IdProductDetail = a.IdProductDetail,
                           IdBill = a.IdBill,
                           Ten = b.Name,
                           SoLuong = a.SoLuong,
                           TrangThai = a.TrangThai
                       }
                       ).ToList();
            return lst.Where(c => c.IdBill == id);
        }

        // GET api/<BillDetailsController>/5
        [HttpGet("{id}")]
        public IEnumerable<BillDetail> Get(Guid id)
        {
            return BillRepo.GetAll().Where(c => c.IdBill == id).ToList();
        }

        // POST api/<BillDetailsController>
        [HttpPost]
        public string Post(Guid idBill, Guid idProduct, int sl, int trangthai)
        {
            var b = BillRepo.GetAll().FirstOrDefault(c => c.IdBill == idBill && c.IdProductDetail == idProduct);
            var c = ProductDetailRepo.GetAll().FirstOrDefault(a => a.Id == idProduct);
            if (b != null)
            {
                b.SoLuong = b.SoLuong + sl;
                if (b.SoLuong > c.SoLuongTon)
                {
                    return "khum du so luong";
                }
                if (BillRepo.EditItem(b))
                    return "san pham nay da co tron bill va da duoc cap nhap";
                return "khong thanh cong";

            }
            var d = new BillDetail() { Id = Guid.NewGuid(), IdBill = idBill, IdProductDetail = idProduct, DonGia = c.GiaBan, SoLuong = sl, TrangThai = trangthai };
            if (BillRepo.AddItem(d)) return "Them thanh cong";
            return "them khong thanh cong";
        }

        // PUT api/<BillDetailsController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, Guid idBill, Guid idProduct, int sl, int trangthai)
        {
            var c = ProductDetailRepo.GetAll().FirstOrDefault(a => a.IdProduct == a.IdProduct);
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
