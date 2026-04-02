using System.ComponentModel.DataAnnotations;

namespace REST_API_Shashin.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int orderId { get; set; }
        public int dishId { get; set; }
        public int count { get; set; }
    }
}
