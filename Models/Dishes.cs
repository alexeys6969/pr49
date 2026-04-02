using System.ComponentModel.DataAnnotations;

namespace REST_API_Shashin.Models
{
    public class Dishes
    {
        [Key]
        public int dishId {  get; set; }
        public string category { get; set; }
        public string nameDish { get; set; }
        public string price { get; set; }
        public string icon { get; set; }
        public string version { get; set; }
    }
}
