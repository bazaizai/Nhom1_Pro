using AppData.Models;
using AppView.IServices;
using AppView.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace AppView.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductDetailController : Controller
    {
        private readonly IProductDetailService _productDetailService;
        public ProductDetailController()
        {
            _productDetailService = new ProductDetailService();
        }

        public ActionResult Call()
        {
            return View();
        }
        public async Task<ActionResult> GetAllProduct(string name)
        {
            try
            {
                var getAllProduct = await _productDetailService.GetAll();
                return String.IsNullOrEmpty(name) ? View(getAllProduct) : View(_productDetailService.GetByName(name));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                return await _productDetailService.RemoveItem(id) ? RedirectToAction("GetAllProduct") : Content("Hệ thống đang nâng cấp");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            try
            {
                var data = await _productDetailService.GetAllBienThe() as dynamic;
                ViewBag.Product = data.sanphams;
                ViewBag.Color = data.colors;
                ViewBag.Size = data.sizes;
                ViewBag.Material = data.materials;
                ViewBag.TypeProduct = data.typeProducts;
                return View();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public async Task<ActionResult> Details(Guid id)
        {
            try
            {
                return View(await _productDetailService.GetById(id));
            }
            catch (HttpRequestException)
            {
                return BadRequest();
            }
        }

        public async Task<ActionResult> SearchProduct([FromQuery] string name)
        {
            try
            {
                return PartialView("_PartialViewPrductList", await _productDetailService.GetByName(name));
            }
            catch (HttpRequestException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreatePro([FromBody] ProductDetailViewModel obj)
        {
            try
            {
                HttpResponseMessage response = await _productDetailService.AddItem(obj);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return Content(responseData, "application/json");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();                
            }
        }


        [HttpGet]
        public async Task<ActionResult> ViewEdit(Guid id)
        {
            try
            {
                var data = await _productDetailService.GetAllBienThe() as dynamic;
                ViewBag.Product = data.sanphams;
                ViewBag.Color = data.colors;
                ViewBag.Size = data.sizes;
                ViewBag.Material = data.materials;
                ViewBag.TypeProduct = data.typeProducts;
                return View(await _productDetailService.GetProductUpdate(id));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
