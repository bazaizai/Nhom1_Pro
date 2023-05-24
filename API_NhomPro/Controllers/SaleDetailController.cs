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
    public class SaleDetailController : ControllerBase
    {
        private readonly IAllRepo<SaleDetail> repos;
        DBContextModel context = new DBContextModel();
        DbSet<SaleDetail> SaleDetail;
        public SaleDetailController()
        {
            SaleDetail = context.DetailSales;
            AllRepo<SaleDetail> all = new AllRepo<SaleDetail>(context, SaleDetail);
            repos = all;
        }
        [HttpGet]
        public IEnumerable<SaleDetail> Get()
        {
            return repos.GetAll();
        }

        [HttpPost]
        public bool CreateSaleDetail(string mota, int trangthai, Guid IdSale,Guid IdChiTietSp )
        {
            SaleDetail saleDetail = new SaleDetail();
           saleDetail.IdSale = IdSale;
            saleDetail.IdChiTietSp= IdChiTietSp;
            saleDetail.TrangThai = trangthai;
            saleDetail.Id = Guid.NewGuid();
            saleDetail.MoTa= mota;
            return repos.AddItem(saleDetail);
        }


        [HttpPut("{id}")]
        public bool Put(Guid id, string mota, int trangthai, Guid IdSale, Guid IdChiTietSp)
        {
            var SaleDetail = repos.GetAll().First(p => p.Id == id);
            SaleDetail.IdSale=IdSale;
            SaleDetail.IdChiTietSp = IdChiTietSp;
            SaleDetail.TrangThai = trangthai;
            SaleDetail.MoTa= mota;
            return repos.EditItem(SaleDetail);
        }

        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var SaleDetail = repos.GetAll().First(p => p.Id == id);
            return repos.RemoveItem(SaleDetail);
        }
    }
}
