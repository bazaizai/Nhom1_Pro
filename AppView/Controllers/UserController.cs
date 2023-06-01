using AppData.IRepositories;
using AppData.Repositories;
using AppView.Services;
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
        UserServices usersService;
        public UserController()
        {
            Users = dbContextModel.Users;
            Roles = dbContextModel.Roles;
            AllRepo<User> all = new AllRepo<User>(dbContextModel, Users);
            AllRepo<Role> allrole = new AllRepo<Role>(dbContextModel, Roles);
            repos = all;
            repo = allrole;
            usersService = new UserServices();
        }
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllUser(string sreach)
        {
            var a = await usersService.GetAllUser();
            if (sreach != null)
            {
                var user = a.Where(c => c.Ten.ToUpper().Contains(sreach.ToUpper()));
                return View(user);
            }
            else
            {
                return View(a);
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
        public async Task<IActionResult> Create(User user)
        {
            await usersService.AddUser(user);
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
            await usersService.Edit(user);
            return RedirectToAction("GetAllUser");
        }

        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            await usersService.DeleteUser(id);
            return RedirectToAction("GetAllUser");
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
