namespace EStore.Models.DTOs
{
    public class DashboardVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
