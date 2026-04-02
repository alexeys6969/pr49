using System.ComponentModel.DataAnnotations;

namespace REST_API_Shashin.Models
{
    public class Order
    {
        [Key]
        public int orderId { get; set; }
        public string address { get; set; }
        public string date { get; set; }
    }
}
