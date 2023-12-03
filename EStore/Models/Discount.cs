using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EStore.Models
{
    [Table("Discount")]
    public class Discount
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string DiscountCode { get; set; }
        [Required]
        public int DiscountAmount { get; set; }
    }
}
