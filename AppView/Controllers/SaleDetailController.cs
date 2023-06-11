using AppData.IRepositories;
using AppData.Models;
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
        ISaleDetailService SaleDetailService;
        IProductDetailService productDetailService;
        ISaleService SaleService;
        public SaleDetailController()
        {
            SaleDetailService = new SaleDetailService();
            productDetailService = new ProductDetailService();
            SaleService = new SaleService();
        }

        public async Task<IActionResult> GetAllSaleDetail()
        {
            var SaleDetails = await SaleDetailService.GetAllDetaiSale();
            return View(SaleDetails);
        }
        public async Task<IActionResult> DetailSaleDetail(Guid id)
        {
            var sp = (await SaleDetailService.GetAllDetaiSale()).FirstOrDefault(x => x.Id == id);
            return View(sp);
        }


        public async Task<IActionResult> CreateSaleDetail()
        {
            var sp = (await SaleDetailService.getallSpSale()).Where(x => x.TrangThaiSale == 0 || x.TrangThaiSale == null);
            ViewBag.ProductList = sp;
            var filteredSales = (await SaleService.GetAllSale()).Where(s => s.TrangThai == 0).ToList();
            ViewData["IdSale"] = new SelectList(filteredSales, "Id", "Ten");
            SelectList salesList = new SelectList(filteredSales, "Id", "Ten");
            ViewBag.SaleList = salesList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSaleDetail(SaleDetail s, List<string> selectedProducts)
        {
            List<SaleDetail> saleDetails = await SaleDetailService.GetAllDetaiSale();
            List<Sale> lstsales = (await SaleService.GetAllSale());
            s.TrangThai = 0;
            var lstsp = await SaleDetailService.getallSpSale();
            if (selectedProducts != null && selectedProducts.Count > 0)
            {
                foreach (var productid in selectedProducts)
                {
                    if (saleDetails.Find(a => a.IdSale == s.IdSale && a.IdChiTietSp == Guid.Parse(productid)) != null)
                    {
                        continue;
                    }
                    else
                    {
                        if (saleDetails.Find(a => a.IdChiTietSp == Guid.Parse(productid)) == null)// sp chưa có sale
                        {
                            s.IdChiTietSp = Guid.Parse(productid);
                            await SaleDetailService.CreateDetaiSale(s);
                        }
                        else
                        {

                            var a = saleDetails.Find(a => a.IdChiTietSp == Guid.Parse(productid));
                            //await SaleDetailService.DeleteDetaiSale(a.Id);
                            a.TrangThai = 1;// 0 hoạt động
                            await SaleDetailService.EditDetaiSale(a);
                            //foreach (var p in lstsp)
                            //{
                            //    var m = productSales.Where(x => x.NgayKetThuc != null && x.TrangThaiSale != null);
                            //    if (m.First(m => m.TrangThaiSale == 0) != null)
                            //    {
                            //        int a = 1;
                            //    }
                            //    else
                            //    {
                            //        DateTime? maxvalue = DateTime.MinValue;
                            //        productSale a = null;
                            //        foreach (var q in m)
                            //        {
                            //            if (q.NgayKetThuc > maxvalue)
                            //            {
                            //                maxvalue = q.NgayKetThuc;
                            //                a = q;
                            //            }
                            //        }
                            //        if (maxvalue > DateTime.Now)
                            //        {
                            //            var n = saledetails.Find(x => x.IdChiTietSp == a.Id);
                            //            n.TrangThai = 0;// hoạt động
                            //            await EditDetaiSale(n);
                            //        }
                            //    }
                        }






                        s.IdChiTietSp = Guid.Parse(productid);
                        await SaleDetailService.CreateDetaiSale(s);
                    }


                }
            }
            return RedirectToAction("GetAllSaleDetail");
        }





        public async Task<IActionResult> DeleteSaleDetail(Guid id)
        {
            if (await SaleDetailService.DeleteDetaiSale(id))
            {
                return RedirectToAction("GetAllSaleDetail");
            }
            else return BadRequest();
        }
        public async Task<IActionResult> EditSaleDetail(Guid id)
        {
            IEnumerable<SelectListItem> salesList = new SelectList(await SaleService.GetAllSale(), "Id", "Ten");
            ViewBag.SaleList = salesList;

            IEnumerable<SelectListItem> productDetailsList = new SelectList(await productDetailService.GetAll(), "Id", "Id");
            ViewBag.ProductDetailsList = productDetailsList;
            ViewBag.SaleList = salesList;
            var sp = (await SaleDetailService.GetAllDetaiSale()).FirstOrDefault(x => x.Id == id);

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
