using AppData.IRepositories;
using AppData.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using System.Drawing;

namespace AppView.Controllers
{
    public class UserController : Controller
    {
        private readonly IAllRepo<User> repos;
        private readonly IAllRepo<Role> repo;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<User> Users;
        DbSet<Role> Roles;
        public UserController()
        {
            Users = dbContextModel.Users;
            Roles = dbContextModel.Roles;
            AllRepo<User> all = new AllRepo<User>(dbContextModel, Users);
            AllRepo<Role> allrole = new AllRepo<Role>(dbContextModel, Roles);
            repos = all;
            repo = allrole;
        }
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllUser(string sreach)
        {
            string ạpiUrl = "https://localhost:7280/api/User";
            var httpClient = new HttpClient(); // tạo ra để callApi
            var response = await httpClient.GetAsync(ạpiUrl);
            // Lấy dữ liệu Json trả về từ Api được call dạng string
            string apiData = await response.Content.ReadAsStringAsync();
            // Đọc từ string Json vừa thu được sang List<T>
            var users = JsonConvert.DeserializeObject<List<User>>(apiData);
            if (sreach != null)
            {
                var user = users.Where(c => c.Ten.ToUpper().Contains(sreach.ToUpper()));
                return View(user);
            }
            else
            {
                return View(users);
            }
        }
        // GET: UserController/Details/5
        public ActionResult Details(Guid id)
        {
            var user = repos.GetAll().FirstOrDefault(c => c.Id == id);
            return View(user);
        }

        // GET: UserController/Create
        public async Task<IActionResult> Create()
        {
            string apiUrl = "https://localhost:7280/api/Role";
            var httpClient = new HttpClient(); // tạo ra để callApi
            var response = await httpClient.GetAsync(apiUrl);
            // Lấy dữ liệu Json trả về từ Api được call dạng string
            string apiData = await response.Content.ReadAsStringAsync();
            // Đọc từ string Json vừa thu được sang List<T>
            var roles = JsonConvert.DeserializeObject<List<Role>>(apiData);
            ViewBag.Role = new SelectList(roles, "Id", "Ten");
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid id, Guid idRole, string ten, int gioitinh, DateTime ngaysinh, string diachi, string sdt, string matkhau, string email, string taikhoan, int trangthai, string mota)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/User/Create-User?idRole={idRole}&ten={ten}&GioiTinh={gioitinh}&NgaySinh={ngaysinh}&diachi={diachi}&sdt={sdt}&matkhau={matkhau}&email={email}&taikhoan={taikhoan}&trangthai={trangthai}";
            var response = await httpClient.PostAsync(apiUrl, null);
            return RedirectToAction("GetAllUser");
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            string apiUrl = "https://localhost:7280/api/Role";
            var httpClient = new HttpClient(); // tạo ra để callApi
            var response = await httpClient.GetAsync(apiUrl);
            // Lấy dữ liệu Json trả về từ Api được call dạng string
            string apiData = await response.Content.ReadAsStringAsync();
            // Đọc từ string Json vừa thu được sang List<T>
            var roles = JsonConvert.DeserializeObject<List<Role>>(apiData);
            ViewBag.Role = new SelectList(roles, "Id", "Ten");
            var user = repos.GetAll().FirstOrDefault(c => c.Id == id);
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/User/Create-User?idRole=e{user.IdRole}&ten={user.Ten}&GioiTinh={user.GioiTinh}&NgaySinh={user.NgaySinh}&diachi={user.DiaChi}&sdt={user.Sdt}&matkhau={user.MatKhau}&email={user.Email}&taikhoan={user.TaiKhoan}&trangthai={user.TrangThai}";
            var response = await httpClient.PutAsync(apiUrl, null);
            return RedirectToAction("GetAllUser");
        }

        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/User/Delete-User?id={id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return RedirectToAction("GetAllRole");
        }

        // POST: UserController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
