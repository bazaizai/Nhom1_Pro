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
        private readonly IAllRepo<Sale> repos;
        DBContextModel context = new DBContextModel();
        DbSet<Sale> sale;
        ISaleService SaleService;
        public SaleController()
        {
            sale = context.Sales;
            AllRepo<Sale> all = new AllRepo<Sale>(context, sale);
            repos = all;
            SaleService = new SaleService();
        }
        // GET: SaleController
        public async Task<IActionResult> GetAllSale()
        {

            var sales = await SaleService.GetAllSale();
            return View(sales);
        }

        // GET: SaleController/Details/5
        public ActionResult DetailSale(Guid id)
        {
            var sale = repos.GetAll().FirstOrDefault(x => x.Id == id);
            return View(sale);
        }

        // GET: SaleController/Create
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
        public IActionResult EditSale(Guid id)
        {
            var sp = repos.GetAll().FirstOrDefault(x => x.Id == id);

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
