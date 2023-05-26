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
        private readonly IAllRepo<BillDetail> allRepo;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<BillDetail> BillDetails;

        public BillDetailsController()
        {
            BillDetails = dbContextModel.BillDetails;
            AllRepo<BillDetail> all = new AllRepo<BillDetail>(dbContextModel,BillDetails);
            allRepo = all;
        }
        // GET: api/<BillDetailsController>
        [HttpGet]
        public IEnumerable<BillDetail> Get()
        {
            return allRepo.GetAll().ToList();
        }

        // GET api/<BillDetailsController>/5
        [HttpGet("{id}")]
        public IEnumerable<BillDetail> Get(Guid id)
        {
            return allRepo.GetAll().Where(c=>c.IdBill==id).ToList();
        }

        // POST api/<BillDetailsController>
        [HttpPost]
        public bool Post(Guid idBill, Guid idProduct, int sl, int dongia, int trangthai)
        {
            var a = new BillDetail() {Id = Guid.NewGuid(), IdBill = idBill, IdProductDetail = idProduct, DonGia = dongia, SoLuong = sl, TrangThai = trangthai };
            return allRepo.AddItem(a);
        }

        // PUT api/<BillDetailsController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, Guid idBill, Guid idProduct, int sl, int dongia, int trangthai)
        {

            var a = new BillDetail() { Id = id, IdBill = idBill, IdProductDetail = idProduct, DonGia = dongia, SoLuong = sl, TrangThai = trangthai };
            return allRepo.EditItem(a);
        }

        // DELETE api/<BillDetailsController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            return allRepo.RemoveItem(allRepo.GetAll().FirstOrDefault(c => c.Id == id));
        }
    }
}
