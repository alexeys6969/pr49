using System.ComponentModel.DataAnnotations;

namespace REST_API_Shashin.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string login { get; set; }
    }
}
