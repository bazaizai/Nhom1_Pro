using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using AppView.IServices;
using AppView.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using System.Linq;

namespace AppView.Controllers
{
    public class BillController : Controller
    {
        public IAllRepo<Bill> allRepo;
        public IBillService billService;

        private readonly IUserServices userServices;
        private readonly ICartServices cartServices;
        private readonly IRoleServices roleServices;
        private IBillDetailServices billDetailServices;
        public BillController()
        {
            allRepo = new AllRepo<Bill>();
            billService = new BillService();
            userServices = new UserServices();
            cartServices = new CartServices();
            roleServices = new RoleServices();
            billDetailServices = new BillDetailServices();
        }
        public async Task<IActionResult> GetAllBill()
        {
            var a = await billService.GetAllBillsAsync();
            ViewBag.Bills = a;
            return View(a);
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
        public async Task<IActionResult> SearchBill(DateTime startDate, DateTime endDate, string ma)
        {
            if(startDate.Year!=1&& endDate.Year!=1)
            {
                if (ma != null && ma != "")
                {
                    ViewBag.Bills = await billService.GetBillsByDateRangeAsync(startDate, endDate, ma);
                    return PartialView("SearchBill");
                }
                else
                    ViewBag.Bills = await billService.GetBillsByDateRangeAsync(startDate, endDate);
                return PartialView("SearchBill");
            }
            if (ma != null && ma != "")
            {
                ViewBag.Bills = (await billService.GetAllBillsAsync()).Where(x => x.Ma.Contains(ma)).ToList();
                return PartialView("SearchBill");
            }
            else
                ViewBag.Bills = await billService.GetAllBillsAsync();
            return PartialView("SearchBill");
        }
        public async Task<IActionResult> ShowBillForUser()
        {
            var acc = SessionServices.GetObjFromSession(HttpContext.Session, "acc");
            var UserID = (await userServices.GetAllUser()).FirstOrDefault(c => c.TaiKhoan == acc.TaiKhoan).Id;
            List<Bill> billList = (await billService.GetAllBillsAsync()).Where(c=>c.IdUser==UserID).OrderByDescending(c => c.NgayTao).ToList();
            return View(billList);
        }
        public async Task<IActionResult> ShowBillDetails(Guid id)
        {
            List<BillDetailView> bills =await billDetailServices.GetByBill(id);
            return View(bills);
        }
        public async Task<IActionResult> FilterBills(DateTime startDate, DateTime endDate, string ma)
        {
            
            if (startDate.Year != 1 && endDate.Year != 1)
            {
                if (ma != null && ma != "")
                {
                    ViewBag.Date = await billService.GetBillsByDateRangeAsync(startDate, endDate, ma);
                    return PartialView("FilterBills");
                }
                else
                    ViewBag.Date = await billService.GetBillsByDateRangeAsync(startDate, endDate);
            }
            else
                ViewBag.Date = await billService.GetAllBillsAsync();
            return PartialView("FilterBills");
        }

    }
}
