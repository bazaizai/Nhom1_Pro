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
    public class VoucherViewController : Controller
    {
        // GET: VoucherController1
        private IVoucherServices voucherServices;
    


        public VoucherViewController()
        {
            voucherServices = new VoucherServices();
           
        }

        public async Task<IActionResult> IndexAsync()
        {
            var lst = await voucherServices.GetAllAsync();
            return View(lst);
        }

        // GET: VoucherController1/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: VoucherController1/Create
        public async Task<ActionResult> CreateAsync()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync(Voucher voucher)
        {           
            await voucherServices.AddItemAsync(voucher);
            return RedirectToAction("Index");
        }
        // GET: VoucherController1/Edit/5
        public async Task<ActionResult> EditAsync(Guid id)
        {
            var a = (await voucherServices.GetAllAsync()).FirstOrDefault(c => c.Id == id);
            return View(a);
        }

        // POST: VoucherController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Voucher voucher)
        {
            await voucherServices.EditItem(voucher);   
            return RedirectToAction("Index");
        }

        //GET: VoucherController1/Delete/5
        public async Task<ActionResult> DetailAsync(Guid id)
        {
            var a = (await voucherServices.GetAllAsync()).FirstOrDefault(c => c.Id == id);
            return View(a);
        }

        // POST: VoucherController1/Delete/5
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await voucherServices.RemoveItem((await voucherServices.GetAllAsync()).FirstOrDefault(x=>x.Id==id));
            return RedirectToAction("Index");
        }
    }
}
