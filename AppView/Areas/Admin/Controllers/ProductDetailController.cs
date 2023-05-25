using AppData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AppView.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductDetailController : Controller
    {
        public async Task<ActionResult> GetAllProduct(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("https://localhost:7280/api/SanPhamCT/list-SanPhamCT");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<List<ProductDetailDTO>>(responseContent);
                        return View(data);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:7280/api/SanPhamCT/Get-ProductDetailSearch?name={name}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<List<ProductDetailDTO>>(responseContent);
                        return View(data);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
        }
        public async Task<ActionResult> Delete(Guid id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7280/api/SanPhamCT/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsAsync<bool>();
                    if (responseContent)
                    {
                        return RedirectToAction("GetAllProduct");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
        }


        [HttpGet]
        public async Task<ActionResult> Create()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7280/api/SanPhamCT/list-BienThe");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    ViewBag.Product = data.sanphams;
                    ViewBag.Color = data.colors;
                    ViewBag.Size = data.sizes;
                    ViewBag.Material = data.materials;
                    ViewBag.TypeProduct = data.typeProducts;
                    return View();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        public async Task<ActionResult> CreatePro(ProductDetailViewModel obj)
        {
            using (HttpClient client = new HttpClient())
            {
                var apiUrl = "https://localhost:7280/api/SanPhamCT/Create-ProductDetail";
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Xử lý phản hồi thành công từ API
                    var result = await response.Content.ReadAsAsync<bool>();
                    if (result)
                    {
                        return RedirectToAction("GetAllProduct");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
        }


        [HttpGet]
        public async Task<ActionResult> ViewEdit(Guid id)
        {
            using (HttpClient client1 = new HttpClient())
            using (HttpClient client2 = new HttpClient())
            {
                var response1 = await client1.GetAsync("https://localhost:7280/api/SanPhamCT/list-BienThe");
                var response2 = await client2.GetAsync($"https://localhost:7280/api/SanPhamCT/Get-ProductDetailPut?id={id}");

                if (response1.IsSuccessStatusCode && response2.IsSuccessStatusCode)
                {
                    var dataViewBag = await response1.Content.ReadAsAsync<dynamic>();
                    var dataModel = await response2.Content.ReadAsAsync<ProductDetailPutViewModel>();
                    ViewBag.Product = dataViewBag.sanphams;
                    ViewBag.Color = dataViewBag.colors;
                    ViewBag.Size = dataViewBag.sizes;
                    ViewBag.Material = dataViewBag.materials;
                    ViewBag.TypeProduct = dataViewBag.typeProducts;
                    return View(dataModel);
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProductDetail(ProductDetailPutViewModel obj)
        {
            using (HttpClient client = new HttpClient())
            {
                var apiUrl = "https://localhost:7280/api/SanPhamCT/Update-ProductDetail";
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    bool success = JsonConvert.DeserializeObject<bool>(result);
                    if (success)
                    {
                        return RedirectToAction("GetAllProduct");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
        }
    }
}
