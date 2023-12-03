namespace EStore.Models.DTOs
{
    public class ProductDetailVM
    {
        public Product ProductDetail { get; set; }
        public IEnumerable<Product> RelatedProducts { get; set; }
    }
}
