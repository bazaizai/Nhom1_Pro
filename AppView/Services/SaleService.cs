using AppView.IServices;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using System.Drawing;

namespace AppView.Services
{
    public class SaleService : ISaleService
    {
        
        SaleDetailService saleDetailService;     
        private bool isRunning;
        private Thread autoUpdateThread;

        public SaleService()
        {
            isRunning = false;
            saleDetailService=new SaleDetailService();
        }
        public async Task<Sale> GetById(Guid id)
        {
            return (await GetAllSale()).FirstOrDefault(x => x.Id == id);
        }
        public async Task<bool> CreateSale(Sale p)
        {
            
            string apiUrl = $"https://localhost:7280/api/Sale?ma={p.Ma}&ten={p.Ten}&ngaybatdau={p.NgayBatDau}&ngayketthuc={p.NgayKetThuc}&LoaiHinhKm={p.LoaiHinhKm}&mota={p.MoTa}&mucgiam={p.MucGiam}";
            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync(apiUrl, null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            else return false;
        }

        public async Task<bool> DeleteSale(Guid id)
        {
            string apiUrl = $"https://localhost:7280/api/Sale/{id}";
            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else return false;
        }

        public async Task<bool> EditSale(Sale p)
        {
            string apiUrl = $"https://localhost:7280/api/Sale/{p.Id}?ma={p.Ma}&ten={p.Ten}&ngaybatdau={p.NgayBatDau}&ngayketthuc={p.NgayKetThuc}&LoaiHinhKm={p.LoaiHinhKm}&mota={p.MoTa}&mucgiam={p.MucGiam}&trangthai={p.TrangThai}";
            var httpClient = new HttpClient();

            var content = new StringContent(string.Empty);

            var response = await httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            else return false;
        }

        public async Task<List<Sale>> GetAllSale()
        {
            string apiUrl = "https://localhost:7280/api/Sale";
            var httpClient = new HttpClient(); 
            var response = await httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var sales = JsonConvert.DeserializeObject<List<Sale>>(apiData);
            return sales;
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
                // Lấy danh sách sale từ API
                List<Sale> sales = await GetAllSale();
                var lstSaleDetail = await saleDetailService.GetAllDetaiSale();
                // Kiểm tra và cập nhật trạng thái sale
                foreach (var sale in sales)
                {
                    if (sale.NgayKetThuc <= DateTime.Now)
                    {
                        sale.TrangThai = 1;// k hoạt động
                        await EditSale(sale);
                        foreach (var detalsale in lstSaleDetail)
                        {
                            if (detalsale.IdSale == sale.Id)
                            {
                                detalsale.TrangThai = 1;// k hoạt động
                                await saleDetailService.EditDetaiSale(detalsale);
                            }
                        }
                    }                   
                }
                // Ngủ trong một khoảng thời gian (ví dụ: 1 phút) trước khi kiểm tra lại
                await Task.Delay(TimeSpan.FromSeconds(60));
            }
        }
    }
}
