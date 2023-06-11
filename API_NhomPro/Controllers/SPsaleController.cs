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
    public class SPsaleController : ControllerBase
    {
        DBContextModel context = new DBContextModel();

        [HttpGet]
        public IEnumerable<productSale> GetProductsale()
        {          
            var result = from p in context.ProductDetails
                         join sp in context.Products on p.IdProduct equals sp.Id
                         join ps in context.DetailSales on p.Id equals ps.IdChiTietSp into psJoin
                         from ps in psJoin.DefaultIfEmpty()
                         join s in context.Sales on (ps != null ? ps.IdSale : Guid.Empty) equals s.Id into sJoin
                         from s in sJoin.DefaultIfEmpty()
                         select new productSale
                         {
                             TenSP = sp.Ten,
                             TenSale = (s != null ? s.MucGiam + " " + s.LoaiHinhKm : null),
                             Id = p.Id,
                             GiaBan = p.GiaBan,
                             LoaiHinhKm = (s != null ? s.LoaiHinhKm : null),
                             MucGiam = (s != null ? s.MucGiam : null),
                             TrangThaiSale = ps.TrangThai,
                             IdSale = s.Id,
                             MoTa=s.MoTa,
                             NgayKetThuc = s.NgayKetThuc,
                             IdSaleDetai=ps != null ? ps.Id : Guid.Empty,
                         };
            return result.ToList();
        }
    }
}
