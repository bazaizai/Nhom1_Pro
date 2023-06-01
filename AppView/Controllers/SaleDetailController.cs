using AppData.IRepositories;
using AppData.Repositories;
using AppView.IServices;
using AppView.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nhom1_Pro.Models;

namespace AppView.Controllers
{
    public class SaleDetailController : Controller
    {
        private readonly IAllRepo<SaleDetail> repos;
        private readonly IAllRepo<Product> repoproduct;
        DBContextModel context = new DBContextModel();
        DbSet<SaleDetail> saleDetail;
        ISaleDetailService SaleDetailService;
        public SaleDetailController()
        {
            saleDetail = context.DetailSales;
            AllRepo<SaleDetail> all = new AllRepo<SaleDetail>(context, saleDetail);
            repos = all;
            SaleDetailService = new SaleDetailService();
        }

        public async Task<IActionResult> GetAllSaleDetail()
        {
            var SaleDetails = await SaleDetailService.GetAllDetaiSale();
            return View(SaleDetails);
        }
        public IActionResult DetailSaleDetail(Guid id)
        {
            var sp = repos.GetAll().FirstOrDefault(x => x.Id == id);
            return View(sp);
        }


        public IActionResult CreateSaleDetail()
        {
            var listsp = context.ProductDetails.Include("Product").ToList();
            IEnumerable<SelectListItem> salesList = new SelectList(context.Sales, "Id", "Ten");
            ViewBag.SaleList = salesList;
            IEnumerable<SelectListItem> productDetailsList = listsp.Select(pd => new SelectListItem
            {
                Value = pd.Id.ToString(),
                Text = pd.Product.Ten // Truy cập thuộc tính navigation để lấy tên sản phẩm
            });

            ViewBag.ProductDetailsList = productDetailsList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSaleDetail(SaleDetail p)
        {
            if (await SaleDetailService.CreateDetaiSale(p))
            {
                return RedirectToAction("GetAllSaleDetail");
            }
            else return BadRequest();


        }


        public async Task<IActionResult> DeleteSaleDetail(Guid id)
        {
            if (await SaleDetailService.DeleteDetaiSale(id))
            {
                return RedirectToAction("GetAllSaleDetail");
            }
            else return BadRequest();
        }
        public IActionResult EditSaleDetail(Guid id)
        {
            IEnumerable<SelectListItem> salesList = new SelectList(context.Sales, "Id", "Ten");
            ViewBag.SaleList = salesList;

            IEnumerable<SelectListItem> productDetailsList = new SelectList(context.ProductDetails, "Id", "Id");
            ViewBag.ProductDetailsList = productDetailsList;
            ViewBag.SaleList = salesList;
            var sp = repos.GetAll().FirstOrDefault(x => x.Id == id);

            if (sp == null)
            {
                return NotFound();
            }

            return View(sp);
        }

        [HttpPost]
        public async Task<IActionResult> EditSaleDetail(SaleDetail p)
        {
            if (await SaleDetailService.EditDetaiSale(p))
            {
                return RedirectToAction("GetAllSaleDetail");
            }
            else return BadRequest();
        }
    }
}
