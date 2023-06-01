using AppView.IServices;
using Newtonsoft.Json;
using Nhom1_Pro.Models;

namespace AppView.Services
{
    public class VoucherServices : IVoucherServices
    {
        public async Task<bool> AddItemAsync(Voucher item)
        {
            var httpclient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Voucher/AddVoucher?ma={item.Ma}&loaihinhkm={item.LoaiHinhKm}&mucuudai={item.MucUuDai}&phamvi={item.PhamVi}&dieukien={item.DieuKien}&soluongton={item.SoLuongTon}&solansudung={item.SoLanSuDung}&ngaybatdau={item.NgayBatDau}&ngayketthuc={item.NgayKetThuc}&trangthai={item.TrangThai}";
            var reponse = await httpclient.PostAsync(apiUrl, null);
            return true;
        }

        public async Task<bool> EditItem(Voucher item)
        {
             string apiUrl = $" https://localhost:7280/api/Voucher/{item.Id}?ma={item.Ma}&loaihinhkm={item.LoaiHinhKm}&mucuudai={item.MucUuDai}&phamvi={item.PhamVi}&dieukien={item.DieuKien}&soluongton={item.SoLuongTon}&solansudung={item.SoLanSuDung}&ngaybatdau={item.NgayBatDau}&ngayketthuc={item.NgayKetThuc}&trangthai={item.TrangThai}";
            var httpclient = new HttpClient();
            var reponse = await httpclient.PutAsync(apiUrl, null);
            return true;
        }

        public async Task<List<Voucher>> GetAllAsync()
        {
            string apiUrl = "https://localhost:7280/api/Voucher/GetVoucher";
            var httpclient = new HttpClient();
            var reponse = await httpclient.GetAsync(apiUrl);
            string apidata = await reponse.Content.ReadAsStringAsync();
            List<Voucher> voucherslst = JsonConvert.DeserializeObject<List<Voucher>>(apidata);
            return voucherslst;
        }

        public async Task<bool> RemoveItem(Voucher item)
        {
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7280/api/Voucher/{item.Id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return true;
        }
    }
}
