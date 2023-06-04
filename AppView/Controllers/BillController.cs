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
        public BillController()
        {
            allRepo = new AllRepo<Bill>();
            billService = new BillService();
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

            var a = (await billService.GetAllBillsAsync()).FirstOrDefault(x => x.Id == id);
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

        public IActionResult Pay(string name, string phone, string address, string tongtien, string phiship)
        {
            var bill = new Bill()
            {
                Id = new Guid(),
                NgayTao = DateTime.Now,
                NgayShip = DateTime.Now.AddDays(2),
                NgayNhan = DateTime.Now.AddDays(4),
                NgayThanhToan = DateTime.Now.AddDays(4),
                TenNguoiNhan = name,
                DiaChi = address,
                Sdt = phone,
                TongTien = decimal.Parse(tongtien),
                SoTienGiam = null,
                TienShip = decimal.Parse(phiship),
                MoTa = "",
                TrangThai = 0
            };
            return RedirectToAction("ShowCart", "Cart");
        }
    }
}
