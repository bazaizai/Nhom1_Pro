namespace Nhom1_Pro.Models
{
    public class CartDetail
    {

        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        public Guid DetailProductID { get; set; }
        public int Soluong { get; set; }
        public decimal Dongia { get; set; }
        public int TrangThai { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ProductDetail  ProductDetail { get; set; }
    }
}
