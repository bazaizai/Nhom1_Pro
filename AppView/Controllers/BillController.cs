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
        private UserServices userServices;
        private BillService BillService;
        private BillDetailServices BillDetailServices;
        private CartDetailServices CartDetailServices;
        private IProductDetailService ProductDetailServices;
        private VoucherServices VoucherServices;
        public BillController()
        {
            allRepo = new AllRepo<Bill>();
            billService = new BillService();
            userServices = new UserServices();
            billService = new BillService();
            BillDetailServices = new BillDetailServices();
            CartDetailServices = new CartDetailServices();
            ProductDetailServices = new ProductDetailService();
            VoucherServices = new VoucherServices();
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

        public async Task<IActionResult> Pay(string name, string phone, string address, string tongtien, string phiship, string voucher)
        {
            decimal tien = Convert.ToDecimal(tongtien);
            decimal ship = Convert.ToDecimal(phiship);
            var acc = HttpContext.Session.GetString("acc");
            var UserID = (await userServices.GetAllUser()).FirstOrDefault(c => c.TaiKhoan == acc).Id;
            var listcart = (await CartDetailServices.GetAllAsync()).Where(c => c.IdUser == UserID);
            var IDvoucher = (await VoucherServices.GetAllAsync(voucher));

            var bill = new Bill()
            {
                Id = Guid.NewGuid(),
                IdUser = UserID,
                IdVoucher = IDvoucher == null ? Guid.Parse("b55cb83a-e559-4e86-bb36-2e0b37d81db0") : IDvoucher.Id,
                NgayTao = DateTime.Now,
                NgayShip = DateTime.Now.AddDays(2),
                NgayNhan = DateTime.Now.AddDays(4),
                NgayThanhToan = DateTime.Now.AddDays(4),
                TenNguoiNhan = name,
                DiaChi = address,
                Sdt = phone,
                TongTien = tien,
                SoTienGiam = IDvoucher.MucUuDai,
                TienShip = ship,
                MoTa = "0",
                TrangThai = 0
            };
            await billService.CreateBillAsync(bill);
            foreach (var item in listcart)
            {
                await BillDetailServices.AddItemAsync(new BillDetail()
                {
                    Id = new Guid(),
                    IdBill = bill.Id,
                    IdProductDetail = item.IdProduct,
                    SoLuong = item.SoLuongCart,
                    DonGia = item.GiaBan,
                    TrangThai = 0
                });
                await CartDetailServices.RemoveItem(item.Id);
                //var product = await ProductDetailServices.GetById(item.IdProduct);
                //product.SoLuongTon -= item.SoLuongCart;
                //await ProductDetailServices.UpdateItem(product);
            }
            return RedirectToAction("ShowCart", "Cart");
        }
    }
}
