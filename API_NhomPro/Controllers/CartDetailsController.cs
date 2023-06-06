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
        }
        // GET: api/<CartDetailsController>
        [HttpGet]
        public IEnumerable<CartViewModel> Get()
        {
            var cartdetail = allRepo.GetAll();
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
                GiaBan = pd.Dongia,
                TrangThai = pd.TrangThai,
                SoLuongCart = pd.Soluong,
                LinkImage = _reposImage.GetAll().Any(x => x.IdProductDetail == pd.DetailProductID) ? _reposImage.GetAll().Where(pro => pro.IdProductDetail == pd.DetailProductID).FirstOrDefault().TenAnh : null,
            });
            return cartDetails;
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
            var a = new CartDetail() { Id = Guid.NewGuid(), UserID = idUser, DetailProductID = idProduct, Dongia = (decimal)c.GiaBan, Soluong = sl, TrangThai = trangthai };
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
