namespace Nhom1_Pro.Models
{
    public class Cart
    {
        public Guid UserID { get; set; }
        public string Mota { get; set; }
        public int TrangThai { get; set; }
        public virtual IEnumerable<CartDetail> cartdetail { get; set; }
        public virtual User User { get; set; }
    }
}
