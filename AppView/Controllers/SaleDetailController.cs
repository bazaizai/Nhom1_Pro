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
            SaleDetailService.StartAutoUpdate();
        }

        public async Task<IActionResult> GetAllSaleDetail()
        {
            Response.Headers.Add("Refresh", "30");
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
            var sp = (await SaleDetailService.getallSpSale()).DistinctBy(p => p.Id).ToList();
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
            var lstID = (await SaleDetailService.GetAllDetaiSale()).Select(x => x.IdChiTietSp).ToList();
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
                            s.IdChiTietSp = Guid.Parse(productid);
                            await SaleDetailService.CreateDetaiSale(s);

                            var r = (await SaleDetailService.getallSpSale()).Where(x => x.Id == Guid.Parse(productid)&&x.NgayKetThuc>=DateTime.Now).ToList();
                            productSale a = null;
                            decimal? minValue = r.Min(q =>
                            {
                                decimal? saleValue = null;
                                if (q.LoaiHinhKm == "%")
                                {
                                    saleValue = q.GiaBan * q.MucGiam / 100;

                                }
                                else if (q.LoaiHinhKm == "Đ")
                                {
                                    saleValue = q.GiaBan - q.MucGiam;

                                }
                                return saleValue;

                            });
                            foreach (var q in r)
                            {
                                decimal? saleValue = null;

                                if (q.LoaiHinhKm == "%")
                                {
                                    saleValue = q.GiaBan * q.MucGiam / 100;
                                }
                                else if (q.LoaiHinhKm == "Đ")
                                {
                                    saleValue = q.GiaBan - q.MucGiam;
                                }

                                if (saleValue == minValue)
                                {
                                    a = q;
                                    break; // Khi đã tìm được giá trị nhỏ nhất, ta có thể thoát khỏi vòng lặp
                                }
                            }



                            if (a != null)
                            {
                                var o = (await SaleDetailService.GetAllDetaiSale()).FirstOrDefault(x => x.Id == a.IdSaleDetai);
                                foreach (var sale in saleDetails)
                                {
                                    
                                    if (sale == o)
                                    {
                                        sale.TrangThai = 0;//hoạt động
                                        await SaleDetailService.EditDetaiSale(sale);
                                    }
                                    else
                                    {
                                        sale.TrangThai = 1;//0 hoạt động
                                        await SaleDetailService.EditDetaiSale(sale);
                                    }
                                }
                            }


                        }

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
