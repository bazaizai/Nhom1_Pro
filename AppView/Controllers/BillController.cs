using AppData.IRepositories;
using AppData.Repositories;
using AppView.IServices;
using AppView.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using System;

namespace AppView.Controllers
{
    public class BillController : Controller
    {
        public IAllRepo<Bill> allRepo;
        public IBillService billService;
        public BillController()
        {
            allRepo = new AllRepo<Bill>();
            billService = new BillService();
        }
        public async Task<IActionResult> GetAllBill()
        {
            ViewBag.Bills = await billService.GetAllBillsAsync();
            return View();
        }
      
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Bill bill)
        {
            await billService.CreateBillAsync(bill);
            return RedirectToAction("GetAllBill");
        }
        public async Task<IActionResult> Details(Guid id)
        {
            
            var a = (await billService.GetAllBillsAsync()).FirstOrDefault(x=>x.Id==id);
            return View(a);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            DBContextModel dBContextModel = new DBContextModel();
            var a = dBContextModel.Bills.Find(id);
            return View(a);
        }

        public async Task<IActionResult> Edit(Bill bill)
        {
            await billService.UpdateBillAsync(bill);
            return RedirectToAction("GetAllBill");
        }
        
        public async Task<IActionResult> Delete(Guid id)
        {
            await billService.DeleteBillAsync(id);
            return RedirectToAction("GetAllBill");
        }
        public async Task<IActionResult> SearchBill(string ma)
        {
            var lst = await billService.GetAllBillsAsync();
            if (ma != null && ma != "")
            {
               var bills = (await billService.GetAllBillsAsync()).Where(x=>x.Ma.Contains(ma)).ToList();
                ViewBag.Bills = bills;
                return PartialView("SearchBill");
            }
            else
                ViewBag.Bills = lst;
            return PartialView("SearchBill");
        }
    }
}
