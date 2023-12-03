namespace EStore.Models.DTOs
{
    public class DiscountVM
    {
        public ShoppingCart ShoppingCart { get; set; }
        public string DiscountCode { get; set; } = "";
        public int DiscountAmount { get; set; } = 0;
    }
}
