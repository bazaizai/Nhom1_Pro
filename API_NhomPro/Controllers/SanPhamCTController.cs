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

        // GET: api/<SanPhamCTController>
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
                ListImage = _reposImage.GetAll().Where(im => im.IdProductDetail == pd.Id).Select(x => x.TenAnh).ToList()
            }); ;
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
            return Ok(selectList);
        }

        [HttpGet("list-SanPham")]
        public IEnumerable<Product> GetAllProduct()
        {
            return _reposSP.GetAll();
        }

        [HttpGet("list-Image")]
        public IEnumerable<Image> GetAllImage()
        {
            return _reposImage.GetAll();
        }

        [HttpGet("list-Size")]
        public IEnumerable<Size> GetAllSize()
        {
            return _reposSize.GetAll();
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
            return productDetailPut;
        }

        [HttpGet("Get-ProductDetailSearch")]
        public IEnumerable<ProductDetailDTO> GetProductSearch(string name)
        {
            return GetAllProductDetail().Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        [HttpGet("list-TypeProduct")]
        public IEnumerable<TypeProduct> GetAllTypeProduct()
        {
            return _reposTypeProduct.GetAll();
        }

        [HttpGet("list-Material")]
        public IEnumerable<Material> GetAllMaterial()
        {
            return _reposMaterial.GetAll();
        }

        [HttpGet("list-Color")]
        public IEnumerable<Color> GetAllColor()
        {
            return _reposColor.GetAll();
        }

        [HttpPost("Create-Product")]
        public bool Create([FromBody] ProductViewModel pro)
        {
            Product product = new Product()
            {
                Ma = _reposSP.MaTS(),
                Ten = pro.Ten,
                TrangThai = pro.TrangThai
            };
            return _reposSP.AddItem(product);
        }

        [HttpPost("Create-Size")]
        public bool Create([FromBody] SizeViewModel size)
        {
            return _reposSize.AddItem(new Size()
            {
                Ma = _reposSize.MaTS(),
                Cm = size.Cm,
                Size1 = size.Size1,
                TrangThai = size.TrangThai
            });
        }

        [HttpPost("Create-Color")]
        public bool Create([FromBody] ColorViewModel color)
        {
            return _reposColor.AddItem(new Color()
            {
                Ma = _reposColor.MaTS(),
                Ten = color.Ten,
                TrangThai = color.TrangThai
            });
        }

        [HttpPost("Create-TypeProduct")]
        public bool Create([FromBody] TypeProductViewModel type)
        {
            return _reposTypeProduct.AddItem(new TypeProduct()
            {
                Ma = _reposTypeProduct.MaTS(),
                Ten = type.Ten,
                TrangThai = type.TrangThai
            });
        }


        [HttpPost("Create-Material")]
        public bool Create([FromBody] MaterialViewModel material)
        {
            return _reposMaterial.AddItem(new Material()
            {
                TrangThai = material.TrangThai,
                Ten = material.Ten,
                Ma = _reposMaterial.MaTS(),
            });
        }

        [HttpPost("Create-ProductDetail")]
        public bool Create([FromBody] ProductDetailViewModel pro)
        {
            pro.TrangThai = 1;
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDetailViewModel, ProductDetail>();
            });

            IMapper mapper = configuration.CreateMapper();
            ProductDetail productDetail = mapper.Map<ProductDetail>(pro);
            return _reposCTSP.AddItem(productDetail);
        }

        [HttpPost("Create-Image")]
        public async Task<bool> CreateImage(Guid idProductDetail,IFormFile  fileImage)
        {
            var image = new Image()
            {
                TenAnh = fileImage.FileName,
                IdProductDetail = idProductDetail,
                TrangThai = 1
            };
            string destinationPath = @"C:\Users\hoang\OneDrive\Máy tính\Net105 - Copy\AppView\wwwroot\assets\images\others\";
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
            return _reposCTSP.RemoveItem(_reposCTSP.GetAll().FirstOrDefault(x => x.Id == id));
        }

        [HttpDelete("image/{id}")]
        public bool DeleteImage(Guid id)
        {
            return _reposImage.RemoveItem(_reposImage.GetAll().FirstOrDefault(x => x.Id == id));
        }

        [HttpPut("Update-ProductDetail")]
        public bool UpdateProductDetail([FromBody] ProductDetailPutViewModel pro)
        {
            pro.TrangThai = 1;
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDetailPutViewModel, ProductDetail>();
            });

            IMapper mapper = configuration.CreateMapper();
            ProductDetail productDetail = mapper.Map<ProductDetail>(pro);

            return _reposCTSP.EditItem(productDetail);
        }

        [HttpPut("Update-Image")]
        public async Task<bool> UpdateImage(Guid id, int trangthai,IFormFile fileImage)
        {
            var image = _reposImage.GetAll().FirstOrDefault(x => x.Id == id);
            image.TenAnh = fileImage.FileName;
            image.TrangThai = trangthai;
            string destinationPath = @"C:\Users\hoang\OneDrive\Máy tính\Net105 - Copy\AppView\wwwroot\assets\images\others\";
            string fileName = Path.GetFileName(fileImage.FileName);
            string destinationFilePath = Path.Combine(destinationPath, fileName);
            using (var stream = new FileStream(destinationFilePath, FileMode.Create))
            {
                await fileImage.CopyToAsync(stream);
            }
            return _reposImage.EditItem(image);
        }
    }
}
