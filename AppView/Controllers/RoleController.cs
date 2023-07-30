using AppData.IRepositories;
using AppData.Repositories;
using AppView.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using System.Data;
using System.Drawing;

namespace AppView.Controllers
{
    public class RoleController : Controller
    {
        private readonly IAllRepo<Role> repos;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<Role> Roles;
        RoleServices RoleServices;
        public RoleController()
        {
            Roles = dbContextModel.Roles;
            AllRepo<Role> all = new AllRepo<Role>(dbContextModel, Roles);
            repos = all;
            RoleServices = new RoleServices();
        }
        // GET: RoleController
        public ActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllRole(string sreach)
        {
            var a = await RoleServices.GetAllRole();
            if (sreach != null)
            {
                var role = a.Where(c => c.Ten.ToUpper().Contains(sreach.ToUpper()));

                return View(role);
            }

            else
            {
                return View(a);
            }

        }
        // GET: RoleController/Details/5
        public ActionResult Details(Guid id)
        {
            var role = repos.GetAll().FirstOrDefault(c => c.Id == id);
            return View(role);
        }

        // GET: RoleController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string ten, int trangthai)
        {
            await RoleServices.AddRole(ten, trangthai);
            return RedirectToAction("GetAllRole");
        }

        // GET: RoleController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var role = repos.GetAll().FirstOrDefault(c => c.Id == id);
            return View(role);
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, string ten, int trangthai)
        {
            await RoleServices.Edit(id, ten, trangthai);
            return RedirectToAction("GetAllRole");
        }

        // GET: RoleController/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            await RoleServices.DeleteRole(id);
            return RedirectToAction("GetAllRole");
        }

        // POST: RoleController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var httpClient = new HttpClient();
        //    string apiUrl = $"https://localhost:7280/api/Role/Delete-Role?id={id}";
        //    var response = await httpClient.DeleteAsync(apiUrl);
        //    return RedirectToAction("GetAllRole");
        //}
    }
}
