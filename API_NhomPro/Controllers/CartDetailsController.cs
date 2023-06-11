using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom1_Pro.Models;
using System.Linq;

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
        private readonly IAllRepo<Product> _reposSP;
        private readonly IAllRepo<Size> _reposSize;
        private readonly IAllRepo<Color> _reposColor;
        private readonly IAllRepo<TypeProduct> _reposTypeProduct;
        private readonly IAllRepo<Material> _reposMaterial;
        private readonly IAllRepo<Image> _reposImage;
        private readonly IAllRepo<ProductDetail> _reposCTSP;
        private readonly IAllRepo<User> _repoUser;
        private readonly IAllRepo<Sale> _repoSale;
        private readonly IAllRepo<SaleDetail> _repoSaleDetail;
        public CartDetailsController()
        {
            CartDetails = dbContextModel.CartDetails;
            ProductDetails = dbContextModel.ProductDetails;
            allRepo = new AllRepo<CartDetail>(dbContextModel, CartDetails);
            ProductRepo = new AllRepo<ProductDetail>(dbContextModel, ProductDetails);
            _reposSP = new AllRepo<Product>();
            _reposSize = new AllRepo<Size>();
            _reposColor = new AllRepo<Color>();
            _reposTypeProduct = new AllRepo<TypeProduct>();
            _reposMaterial = new AllRepo<Material>();
            _reposImage = new AllRepo<Image>();
            _reposCTSP = new AllRepo<ProductDetail>();
            _repoUser = new AllRepo<User>();
            _repoSale = new AllRepo<Sale>();
            _repoSaleDetail = new AllRepo<SaleDetail>();

        }
        // GET: api/<CartDetailsController>
        [HttpGet]
        public IEnumerable<CartViewModel> Get()
        {
            var cartdetail = allRepo.GetAll();
            DBContextModel context = new DBContextModel();
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
                             MoTa = s.MoTa,
                             NgayKetThuc = s.NgayKetThuc,
                             IdSaleDetai = ps != null ? ps.Id : Guid.Empty,
                         };
            var cartDetails = cartdetail.Select(pd => new CartViewModel
            {
                Id = pd.Id,
                IdUser = pd.UserID,
                IdProduct = pd.DetailProductID,
                Name = _reposSP.GetAll().FirstOrDefault(x => x.Id == _reposCTSP.GetAll().FirstOrDefault(p => p.Id == pd.DetailProductID).IdProduct).Ten,
                Size = _reposSize.GetAll().FirstOrDefault(s => s.Id == _reposCTSP.GetAll().FirstOrDefault(p => p.Id == pd.DetailProductID).IdSize)?.Size1,
                Color = _reposColor.GetAll().FirstOrDefault(c => c.Id == _reposCTSP.GetAll().FirstOrDefault(p => p.Id == pd.DetailProductID).IdColor)?.Ten,
                Material = _reposMaterial.GetAll().FirstOrDefault(m => m.Id == _reposCTSP.GetAll().FirstOrDefault(p => p.Id == pd.DetailProductID).IdMaterial)?.Ten,
                TypeProduct = _reposTypeProduct.GetAll().FirstOrDefault(tp => tp.Id == _reposCTSP.GetAll().FirstOrDefault(p => p.Id == pd.DetailProductID).IdTypeProduct)?.Ten,
                GiaBan = _reposCTSP.GetAll().FirstOrDefault(c => c.Id == pd.DetailProductID).GiaBan,
                TrangThai = pd.TrangThai,
                SoLuongCart = pd.Soluong,
                LinkImage = _reposImage.GetAll().Any(x => x.IdProductDetail == pd.DetailProductID) ? _reposImage.GetAll().Where(pro => pro.IdProductDetail == pd.DetailProductID).FirstOrDefault().TenAnh : null,
                IdSale = result.FirstOrDefault(x => x.Id == pd.DetailProductID)?.IdSale,
                TrangThaiSale = result.FirstOrDefault(x => pd.DetailProductID == pd.Id)?.TrangThaiSale,
                IdSaleDetai = result.FirstOrDefault(x => pd.DetailProductID == pd.Id)?.IdSaleDetai,              
                NgayKetThuc = result.FirstOrDefault(x => x.Id == pd.DetailProductID)?.NgayKetThuc,
                TenSale = result.FirstOrDefault(x => x.Id == pd.DetailProductID)?.TenSale,
                LoaiHinhKm = result.FirstOrDefault(x => x.Id == pd.DetailProductID)?.LoaiHinhKm,
                MucGiam = result.FirstOrDefault(x => x.Id == pd.DetailProductID)?.MucGiam,
            });
            return cartDetails;
        }
        [HttpPut("Update-cart")]
        public async Task<bool> UpdateCart1(Guid Id, int soLuongCart)
        {
            var cartUpdate = allRepo.GetAll().FirstOrDefault(x => x.Id == Id);
            if (cartUpdate != null)
            {
                cartUpdate.Soluong = soLuongCart;
                allRepo.EditItem(cartUpdate);
            }
            return true;
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
            //var d=_repoSale.GetAll().FirstOrDefault(a => a.Id == _repoSaleDetail.GetAll().FirstOrDefault(x=>x.IdChiTietSp== idProduct&&x.TrangThai==0).IdSale);
            var saleDetail = _repoSaleDetail.GetAll().FirstOrDefault(x => x.IdChiTietSp == idProduct && x.TrangThai == 0);
            var d = saleDetail != null ? _repoSale.GetAll().FirstOrDefault(a => a.Id == saleDetail.IdSale) : null;

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
            if(d != null) 
            {
                if (d.LoaiHinhKm == "%")
                {
                    var a = new CartDetail()
                    {
                        Id = Guid.NewGuid(),
                        UserID = idUser,
                        DetailProductID = idProduct,
                        Dongia = (decimal)c.GiaBan - (decimal)c.GiaBan * (decimal)d.MucGiam / 100,
                        Soluong = sl,
                        TrangThai = trangthai
                    };
                    if (allRepo.AddItem(a)) return "Them thanh cong";
                    return "them khong thanh cong";
                }
                else if (d.LoaiHinhKm == "Đ")
                {
                    var a = new CartDetail()
                    {
                        Id = Guid.NewGuid(),
                        UserID = idUser,
                        DetailProductID = idProduct,
                        Dongia = (decimal)c.GiaBan - (decimal)d.MucGiam,
                        Soluong = sl,
                        TrangThai = trangthai
                    };
                    if (allRepo.AddItem(a)) return "Them thanh cong";
                    return "them khong thanh cong";
                }
            }
           
            else
            {
                var a = new CartDetail()
                {
                    Id = Guid.NewGuid(),
                    UserID = idUser,
                    DetailProductID = idProduct,
                    Dongia = (decimal)c.GiaBan,
                    Soluong = sl,
                    TrangThai = trangthai
                };
                if (allRepo.AddItem(a)) return "Them thanh cong";
                return "them khong thanh cong";
            }
            return "Them thanh cong";


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

        [HttpPut("udpade")]
        public async Task<bool> UpdateCart(Guid id, int soluong)
        {
            var cartDetail = allRepo.GetAll().FirstOrDefault(c => c.Id == id);
            cartDetail.Soluong = soluong;
            allRepo.EditItem(cartDetail);
            return true;
        }


    }
}
