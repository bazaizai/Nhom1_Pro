using AppData.IRepositories;
using AppData.Repositories;
using AppView.IServices;
using AppView.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nhom1_Pro.Models;

namespace AppView.Controllers
{
    public class BillController : Controller
    {
        public IAllRepo<Bill> allRepo;
        public IBillService billService;

        private readonly IUserServices userServices;
        private readonly ICartServices cartServices;
        private readonly IRoleServices roleServices;
        public BillController()
        {
            allRepo = new AllRepo<Bill>();
            billService = new BillService();
            userServices = new UserServices();
            cartServices = new CartServices();
            roleServices = new RoleServices();
        }
        public async Task<IActionResult> GetAllBill()
        {
            var a = await billService.GetAllBillsAsync();
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
        public async Task<IActionResult> ShowBillForUser()
        {
            var acc = HttpContext.Session.GetString("acc");
            var UserID = (await userServices.GetAllUser()).FirstOrDefault(c => c.TaiKhoan == acc).Id;
            List<Bill> billList = (await billService.GetAllBillsAsync()).Where(c=>c.IdUser==UserID).OrderByDescending(c => c.NgayTao).ToList();
            return View(billList);
        }
    }
}
