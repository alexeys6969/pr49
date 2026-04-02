using Microsoft.AspNetCore.Mvc;
using REST_API_Shashin.Classes;
using REST_API_Shashin.Models;

namespace REST_API_Shashin.Controllers
{
    [Route("/auth")]
    public class UserController : Controller
    {
        DBManager dbManager = new DBManager();

        [Route("/register")]
        [HttpPost]
        public ActionResult Register([FromBody] UserDto registerUser)
        {
            try
            {
                User newUser = new User
                {
                    email = registerUser.email,
                    password = registerUser.password,
                    login = registerUser.login
                };


                if (newUser == null)
                    return StatusCode(401);
                else
                {
                    dbManager.Users.Add(newUser);
                    dbManager.SaveChanges();
                    return Ok("Успешная регистрация");
                }
            }
            catch (Exception exp)
            {
                return StatusCode(501, exp.Message);
            }
        }
        [Route("/login")]
        [HttpPost]
        public ActionResult Login([FromBody] UserLoginDto loginUser)
        {
            try
            {
                User? AuthUser = dbManager.Users.Where(
                    x => x.email == loginUser.email && x.password == loginUser.password
                    ).FirstOrDefault();
                if (AuthUser == null)
                    return StatusCode(401);
                else
                {
                    string Token = JwtToken.Generate(AuthUser);
                    dbManager.SaveChanges();
                    return Ok(new { token = Token });
                }
            }
            catch (Exception exp)
            {
                return StatusCode(501, exp.Message);
            }
        }
    }
}
