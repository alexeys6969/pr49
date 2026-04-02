using System.ComponentModel.DataAnnotations;

namespace REST_API_Shashin.Models
{
    public class CreateOrderDto
    {
        public string address { get; set; }
        public string date { get; set; }
        public List<OrderItem> dishes { get; set; }
    }
}
