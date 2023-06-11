using AppData.Models;
using AppView.IServices;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using NuGet.Versioning;

namespace AppView.Services
{
    public class SaleDetailService : ISaleDetailService
    {
        private bool isRunning;
        private Thread autoUpdateThread;
        SaleService SaleService;
        ProductDetailService productDetailService;

        public SaleDetailService()
        {
            productDetailService = new ProductDetailService();

        }

        public async Task<SaleDetail> GetById(Guid id)
        {
            return (await GetAllDetaiSale()).FirstOrDefault(x => x.Id == id);
        }
        public async Task<bool> CreateDetaiSale(SaleDetail p)
        {
            string apiUrl = $"https://localhost:7280/api/SaleDetail?mota={p.MoTa}&trangthai={p.TrangThai}&IdSale={p.IdSale}&IdChiTietSp={p.IdChiTietSp}";
            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync(apiUrl, null);


            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else return false;
        }

        public async Task<bool> DeleteDetaiSale(Guid id)
        {
            string apiUrl = $"https://localhost:7280/api/SaleDetail/{id}";
            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else return false;

        }

        public async Task<bool> EditDetaiSale(SaleDetail p)
        {
            string apiUrl = $"https://localhost:7280/api/SaleDetail/{p.Id}?mota={p.MoTa}&trangthai={p.TrangThai}&IdSale={p.IdSale}&IdChiTietSp={p.IdChiTietSp}";
            var httpClient = new HttpClient();

            var content = new StringContent(string.Empty);

            var response = await httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            else return false;
        }

        public async Task<List<SaleDetail>> GetAllDetaiSale()
        {
            string apiUrl = "https://localhost:7280/api/SaleDetail";
            var httpClient = new HttpClient(); // tạo ra để callApi
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var SaleDetails = JsonConvert.DeserializeObject<List<SaleDetail>>(apiData);
            return SaleDetails;
        }
        public async Task<List<productSale>> getallSpSale()
        {
            string apiUrl = "https://localhost:7280/api/SPsale";
            var httpClient = new HttpClient(); // tạo ra để callApi
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var SaleSp = JsonConvert.DeserializeObject<List<productSale>>(apiData);
            return SaleSp;
        }
        public void StartAutoUpdate()
        {
            if (!isRunning)
            {
                isRunning = true;
                autoUpdateThread = new Thread(() => AutoUpdateThread());
                autoUpdateThread.Start();
            }
        }


        public void StopAutoUpdate()
        {
            if (isRunning)
            {
                isRunning = false;
                autoUpdateThread.Join();
            }
        }

        private async Task AutoUpdateThread()
        {
            while (isRunning)
            {

                List<SaleDetail> saledetails = await GetAllDetaiSale();
                List<productSale> productSales = await getallSpSale();

                //var lstpro = await productDetailService.GetAll();
                //foreach (var p in lstpro)
                //{
                //    var m = productSales.Where(x => x.NgayKetThuc != null && x.TrangThaiSale != null);

                //        decimal? giasaukhigiam = decimal.MinValue;

                //        DateTime? maxvalue = DateTime.MinValue;
                //    decimal? k;
                //        productSale a = null;
                //        foreach (var q in m)
                //        {
                //            if (q.NgayKetThuc > maxvalue)
                //            {
                //                maxvalue = q.NgayKetThuc;
                //                a = q;
                //            }
                //        }
                //        if (maxvalue > DateTime.Now)
                //        {
                //            var n = saledetails.Find(x => x.IdChiTietSp == a.Id);
                //            n.TrangThai = 0;// hoạt động
                //            await EditDetaiSale(n);
                //        }

                //}
                var lstID = (await productDetailService.GetAll()).DistinctBy(p => p.Id).Select(p => p.Id).ToList();
                foreach (var p in lstID)
                {
                    var r = (await getallSpSale()).Where(x => x.Id == p && x.NgayKetThuc >=DateTime.Now ).ToList();

                    productSale a = null;
                    decimal? minValue = r.Min(q =>
                    {
                        decimal? saleValue = null;
                        if (q.LoaiHinhKm == "%")
                        {
                            saleValue =q.GiaBan- q.GiaBan * q.MucGiam / 100;

                        }
                        else if (q.LoaiHinhKm == "Đ")
                        {
                            saleValue = q.GiaBan - q.MucGiam;

                        }
                        return saleValue;

                    });
                    foreach (var q in r)
                    {
                        decimal? saleValue = null;

                        if (q.LoaiHinhKm == "%")
                        {
                            saleValue = q.GiaBan - q.GiaBan * q.MucGiam / 100;
                        }
                        else if (q.LoaiHinhKm == "Đ")
                        {
                            saleValue = q.GiaBan - q.MucGiam;
                        }

                        if (saleValue == minValue)
                        {
                            a = q;
                            break; // Khi đã tìm được giá trị nhỏ nhất, ta có thể thoát khỏi vòng lặp
                        }
                    }



                    if (a != null)
                    {
                        var o = (await GetAllDetaiSale()).Where(x => x.IdChiTietSp == a.Id).ToList();
                        foreach (var sale in o)
                        {

                            if (sale.Id == a.IdSaleDetai)
                            {
                                sale.TrangThai = 0;//hoạt động
                                await EditDetaiSale(sale);
                            }
                            else
                            {
                                sale.TrangThai = 1;//0 hoạt động
                                await EditDetaiSale(sale);
                            }
                        }
                    }


                }
            }
            await Task.Delay(TimeSpan.FromSeconds(20));
        }
    }
}

