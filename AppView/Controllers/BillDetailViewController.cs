using AppView.IServices;
using AppView.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppView.Controllers
{
    public class BillDetailViewController : Controller
    {
        private IBillDetailServices billDetailServices;
        public BillDetailViewController()
        {
            billDetailServices = new BillDetailServices();
        }

        // GET: BillDetailViewController1
        public async Task<IActionResult> IndexAsync()
        {
            var lst = await billDetailServices.GetAllAsync();
            return View(lst);
        }

        // GET: BillDetailViewController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BillDetailViewController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BillDetailViewController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BillDetailViewController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BillDetailViewController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BillDetailViewController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BillDetailViewController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
