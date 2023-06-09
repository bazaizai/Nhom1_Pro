using AppView.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nhom1_Pro.Models;
using System.Runtime.InteropServices;

namespace AppView.Services
{
    public class BillService : IBillService
    {
        
        public async Task<bool> CreateBillAsync(Bill obj)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiUrl = $"https://localhost:7280/api/Bill?idUser={obj.IdUser}&idVoucher={obj.IdVoucher}&ma={obj.Ma}&ngayTao={obj.NgayTao}&ngayThanhToan={obj.NgayThanhToan}&ngayShip={obj.NgayShip}&ngayNhan={obj.NgayNhan}" +
                    $"&tenNguoiNhan={obj.TenNguoiNhan}&diaChi={obj.DiaChi}&sdt={obj.Sdt}&tongTien={obj.TongTien}&soTienGiam={obj.SoTienGiam}&tienShip={obj.TienShip}&moTa={obj.MoTa}&trangThai={obj.TrangThai}";
                var response = await httpClient.PostAsync(apiUrl, null);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public async Task<bool> DeleteBillAsync(Guid id)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiUrl = $"https://localhost:7280/api/Bill/{id}";
                var response = await httpClient.DeleteAsync(apiUrl);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<Bill>> GetAllBillsAsync()
        {
            string apiUrl = "https://localhost:7280/api/Bill";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);

            string apiData = await response.Content.ReadAsStringAsync();

            var bills = JsonConvert.DeserializeObject<List<Bill>>(apiData);
            return bills;
        }

        public Bill GetBillById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Bill> GetBillsByMa(string name)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> UpdateBillAsync(Bill obj)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiUrl = $"https://localhost:7280/api/Bill/{obj.Id}?idUser={obj.IdUser}&idVoucher={obj.IdVoucher}&ma={obj.Ma}&ngayTao={obj.NgayTao}&ngayThanhToan={obj.NgayThanhToan}&ngayShip={obj.NgayShip}&ngayNhan={obj.NgayNhan}" +
                        $"&tenNguoiNhan={obj.TenNguoiNhan}&diaChi={obj.DiaChi}&sdt={obj.Sdt}&tongTien={obj.TongTien}&soTienGiam={obj.SoTienGiam}&tienShip={obj.TienShip}&moTa={obj.MoTa}&trangThai={obj.TrangThai}";
                var response = await httpClient.PutAsync(apiUrl, null);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<List<Bill>> GetBillsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            // Thực hiện truy vấn vào nguồn dữ liệu để lấy danh sách hóa đơn trong khoảng thời gian đã chỉ định
            // Đây là một ví dụ giả định sử dụng Entity Framework Core để truy vấn cơ sở dữ liệu

            var bills = (await GetAllBillsAsync())
                .Where(b => b.NgayTao >= startDate && b.NgayTao <= endDate)
                .ToList();

            return bills;
        }
    }
}
