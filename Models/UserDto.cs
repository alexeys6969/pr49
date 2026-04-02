namespace REST_API_Shashin.Models
{
    public class UserDto
    {
        public string email { get; set; }
        public string password { get; set; }
        public string login { get; set; }
        public UserDto(string email, string password, string login)
        {
            this.email = email;
            this.password = password;
            this.login = login;
        }
    }
}
