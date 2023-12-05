using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EStore.Models
{
    [Table("Product")]
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? ProductName { get; set; }

        [Required]
        [MaxLength(40)]
        public string? StoreName { get; set; }
        [Required]
        public double Price { get; set; }
        // Thêm thuộc tính để chứa thông tin về tệp hình ảnh
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string? Image { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<OrderDetail> OrderDetails {  get; set; }
        public List<CartDetail> CartDetails { get; set; }



        [NotMapped]
        public string CategoryName {  get; set; }
        [NotMapped]
        public List<SelectListItem>? CategoryList { get; set; }
    }
}
