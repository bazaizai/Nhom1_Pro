using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nhom1_Pro.Models;
using System.Xml.Linq;




// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamCTController : ControllerBase
    {
        private readonly IAllRepo<ProductDetail> _reposCTSP;
        private readonly IAllRepo<Product> _reposSP;
        private readonly IAllRepo<Size> _reposSize;
        private readonly IAllRepo<Color> _reposColor;
        private readonly IAllRepo<TypeProduct> _reposTypeProduct;
        private readonly IAllRepo<Material> _reposMaterial;
        private readonly IAllRepo<Image> _reposImage;
        public SanPhamCTController()
        {
            _reposCTSP = new AllRepo<ProductDetail>();
            _reposSP = new AllRepo<Product>();
            _reposSize = new AllRepo<Size>();
            _reposColor = new AllRepo<Color>();
            _reposTypeProduct = new AllRepo<TypeProduct>();
            _reposMaterial = new AllRepo<Material>();
            _reposImage = new AllRepo<Image>();
        }

        [HttpGet("list-SanPhamCT")]
        public IEnumerable<ProductDetailDTO> GetAllProductDetail()
        {
            var productDetails = _reposCTSP.GetAll();

            var productDetailDTOs = productDetails.Select(pd => new ProductDetailDTO
            {
                Id = pd.Id,
                Name = _reposSP.GetAll().FirstOrDefault(p => p.Id == pd.IdProduct)?.Ten,
                Size = _reposSize.GetAll().FirstOrDefault(s => s.Id == pd.IdSize)?.Size1,
                Color = _reposColor.GetAll().FirstOrDefault(c => c.Id == pd.IdColor)?.Ten,
                Material = _reposMaterial.GetAll().FirstOrDefault(m => m.Id == pd.IdMaterial)?.Ten,
                TypeProduct = _reposTypeProduct.GetAll().FirstOrDefault(tp => tp.Id == pd.IdTypeProduct)?.Ten,
                BaoHanh = pd.BaoHanh,
                GiaBan = pd.GiaBan,
                GiaNhap = pd.GiaNhap,
                MoTa = pd.MoTa,
                SoLuongTon = pd.SoLuongTon,
                TrangThai = pd.TrangThai,
                LinkImage = _reposImage.GetAll().FirstOrDefault(x => x.IdProductDetail == pd.Id)?.TenAnh,
            });
            return productDetailDTOs;
        }

        [HttpPost("GetProductDetail")]
        public IActionResult GetProductDetail([FromBody] ProductDetailPutViewModel obj)
        {
            var productExists = _reposCTSP.GetAll()
                .Any(pro => pro.IdProduct == obj.IdProduct && pro.IdColor == obj.IdColor && pro.IdSize == obj.IdSize && pro.IdTypeProduct == obj.IdTypeProduct && pro.IdMaterial == obj.IdMaterial);

            if (productExists)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductDetail, ProductDetailPutViewModel>());
                var mapper = config.CreateMapper();
                var product = _reposCTSP.GetAll()
                    .FirstOrDefault(pro => pro.IdProduct == obj.IdProduct && pro.IdColor == obj.IdColor && pro.IdSize == obj.IdSize && pro.IdTypeProduct == obj.IdTypeProduct && pro.IdMaterial == obj.IdMaterial);

                // Copy the product object
                var initialProduct = mapper.Map<ProductDetail, ProductDetailPutViewModel>(product);
                initialProduct.LinkImage = _reposImage.GetAll().Any(x => x.IdProductDetail == initialProduct.Id) ? _reposImage.GetAll().Where(pro => pro.IdProductDetail == initialProduct.Id).FirstOrDefault().TenAnh : null;
                return Ok(new { success = productExists, data = initialProduct });
            }

            return Ok(new { success = productExists });
        }

        [HttpGet("list-BienThe")]
        public ActionResult GetAllBienThe()
        {
            var selectList = new
            {
                colors = new SelectList(_reposColor.GetAll().Where(cl => cl.TrangThai == 1).ToList(), "Id", "Ten"),
                sizes = new SelectList(_reposSize.GetAll().Where(si => si.TrangThai == 1).ToList(), "Id", "Size1"),
                sanphams = new SelectList(_reposSP.GetAll().Where(sp => sp.TrangThai == 1).ToList(), "Id", "Ten"),
                materials = new SelectList(_reposMaterial.GetAll().Where(ma => ma.TrangThai == 1).ToList(), "Id", "Ten"),
                typeProducts = new SelectList(_reposTypeProduct.GetAll().Where(ty => ty.TrangThai == 1).ToList(), "Id", "Ten")
            };
            return new OkObjectResult(selectList);
        }

        [HttpGet("Get-ProductDetailPut")]
        public ProductDetailPutViewModel GetProductDTO(Guid id)
        {
            var productDetail = _reposCTSP.GetAll().FirstOrDefault(pro => pro.Id == id);
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDetail, ProductDetailPutViewModel>();
            });

            IMapper mapper = configuration.CreateMapper();
            var productDetailPut = mapper.Map<ProductDetailPutViewModel>(productDetail);
            productDetailPut.LinkImage = _reposImage.GetAll().Any(x => x.IdProductDetail == productDetailPut.Id) ? _reposImage.GetAll().Where(pro => pro.IdProductDetail == productDetailPut.Id).FirstOrDefault().TenAnh : null;
            return productDetailPut;
        }

        [HttpPost("Create-ProductDetail")]
        public IActionResult CreateProductDetail([FromBody] ProductDetailViewModel pro)
        {
            pro.TrangThai = 1;
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDetailViewModel, ProductDetail>();
            });

            IMapper mapper = configuration.CreateMapper();
            ProductDetail productDetail = mapper.Map<ProductDetail>(pro);
            return new OkObjectResult(new { success = _reposCTSP.AddItem(productDetail), id = productDetail.Id });
        }

        [HttpPost("Create-Image")]
        public async Task<bool> CreateImage(Guid idProductDetail, IFormFile fileImage)
        {
            var image = new Image()
            {
                TenAnh = fileImage.FileName,
                IdProductDetail = idProductDetail,
                TrangThai = 1
            };

            string currentDirectory = Directory.GetCurrentDirectory();
            string rootPath = Directory.GetParent(currentDirectory).FullName;
            string destinationPath = Path.Combine(rootPath, "AppView", "wwwroot", "assets", "images", "others");
            string fileName = Path.GetFileName(fileImage.FileName);
            string destinationFilePath = Path.Combine(destinationPath, fileName);

            using (var stream = new FileStream(destinationFilePath, FileMode.Create))
            {
                await fileImage.CopyToAsync(stream);
            }

            return _reposImage.AddItem(image);
        }


        // DELETE api/<SanPhamCTController>/5
        [HttpDelete("{id}")]
        public bool DeleteSP(Guid id)
        {
            _reposImage.GetAll().Where(x => x.IdProductDetail == id).ToList().ForEach(item => _reposImage.RemoveItem(item));
            return _reposCTSP.RemoveItem(_reposCTSP.GetAll().FirstOrDefault(x => x.Id == id));
        }

        [HttpDelete("image/{id}")]
        public bool DeleteImage(Guid id)
        {
            return _reposImage.RemoveItem(_reposImage.GetAll().FirstOrDefault(x => x.Id == id));
        }

        [HttpPut("Update-ProductDetail")]
        public async Task<bool> UpdateProductDetail([FromBody] ProductDetailPutViewModel pro)
        {
            if (ModelState.IsValid)
            {

            }
            var item = _reposCTSP.GetAll().FirstOrDefault(x => x.IdProduct == pro.IdProduct && x.IdColor == pro.IdColor && x.IdSize == pro.IdSize && x.IdTypeProduct == pro.IdTypeProduct && x.IdMaterial == pro.IdMaterial);
            if (item == null || item.Id == pro.Id)
            {
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ProductDetailPutViewModel, ProductDetail>();
                });
                var productDetai = _reposCTSP.GetAll().FirstOrDefault(x => x.Id == pro.Id);
                IMapper mapper = configuration.CreateMapper();
                mapper.Map(pro, productDetai);

                return _reposCTSP.EditItem(productDetai);
            }
            return false;
        }

        [HttpGet("Update-soLuong")]
        public void UpdateSoLuong(Guid Id, int soLuongCart)
        {
            var productUpdate = _reposCTSP.GetAll().FirstOrDefault(x => x.Id == Id);
            if (productUpdate != null)
            {
                productUpdate.SoLuongTon -= soLuongCart;
                _reposCTSP.EditItem(productUpdate);
            }
        }
        

        [HttpPut("Update-Image")]
        public async Task<bool> UpdateImage(Guid idProduct, [FromForm] IFormFile? fileImage)
        {
            Image image = new Image();
            if (fileImage != null && _reposImage.GetAll().Any(x => x.IdProductDetail == idProduct) != null)
            {
                image = _reposImage.GetAll().Where(x => x.IdProductDetail == idProduct).FirstOrDefault();
                image.TenAnh = fileImage.FileName;
                string currentDirectory = Directory.GetCurrentDirectory();
                string rootPath = Directory.GetParent(currentDirectory).FullName;
                string destinationPath = Path.Combine(rootPath, "AppView", "wwwroot", "assets", "images", "others");
                string fileName = Path.GetFileName(fileImage.FileName);
                string destinationFilePath = Path.Combine(destinationPath, fileName);
                using (var stream = new FileStream(destinationFilePath, FileMode.Create))
                {
                    await fileImage.CopyToAsync(stream);
                }
                return _reposImage.EditItem(image);
            }
            return false;
        }


    }
}
