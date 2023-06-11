using AppData.IRepositories;
using AppData.Repositories;
using AppView.IServices;
using AppView.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nhom1_Pro.Models;

namespace AppView.Controllers
{
    public class SaleController : Controller
    {
        
        ISaleService SaleService;
        public SaleController()
        {           
           SaleService = new SaleService();
            SaleService.StartAutoUpdate();
        }
        public async Task<IActionResult> GetAllSale()
        {
            List<Sale> sales = await SaleService.GetAllSale();
            return View(sales);
        }
        public async Task<IActionResult> DetailSale(Guid id)
        {
            var sale = (await SaleService.GetAllSale()).FirstOrDefault(x => x.Id == id);
            return View(sale);
        }

        public ActionResult CreateSale()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSale(Sale p)
        {
            if (await SaleService.CreateSale(p))
            {
                return RedirectToAction("GetAllSale");
            }
            else return BadRequest();
        }



        public async Task<IActionResult> DeleteSale(Guid id)
        {
            if (await SaleService.DeleteSale(id))
            {
                return RedirectToAction("GetAllSale");
            }
            else return BadRequest();
        }
        public async Task<IActionResult> EditSale(Guid id)
        {
            var sp = (await SaleService.GetAllSale()).FirstOrDefault(x => x.Id == id);

            if (sp == null)
            {
                return NotFound();
            }

            return View(sp);
        }

        [HttpPost]
        public async Task<IActionResult> EditSale(Sale p)
        {
            if (await SaleService.EditSale(p))
            {
                return RedirectToAction("GetAllSale");
            }
            else return BadRequest();
        }
    }
}
