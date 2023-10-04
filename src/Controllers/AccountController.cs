using Microsoft.AspNetCore.Mvc;
using src.DataSource;
using src.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using src.Email;
using Email;
using System.Text;
using src.Filters;

namespace src.Controllers
{
    public class AccountController : Controller
    {
        MainContext db;
        IEmailSender emailSender;
        IMemoryCache cache;
        public AccountController(MainContext db, IEmailSender emailSender, IMemoryCache cache)
        {
            this.db = db;
            this.emailSender = emailSender;
            this.cache = cache;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("/login")]
        [LoginFilter]
        public async Task<IActionResult> Login([FromForm] LoginUser formuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad data");
            }
            
            User? user = db.Users.FirstOrDefault(u => u.Username == formuser.Username && u.Password == formuser.Password);
            if (user is null)
                return NotFound("User not fount");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("Id", user.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var context = ControllerContext.HttpContext; ;
            await context.SignInAsync(claimsPrincipal);

            cache.Set(user.Id, user, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(20)));

            return Ok();
        }

        [Route("/logout")]
        [LogoutFilter]
        public async Task<IActionResult> Logout()
        {
            var context = ControllerContext.HttpContext;
            cache.Remove(GetUserId());
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [HttpGet]
        [Route("/registration")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [Route("/registration")]
        public async Task<IActionResult> Registration([FromForm] RegistrationUser formuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad data");
            }

            if (db.Users.Any(u => u.Username == formuser.Username))
                return BadRequest("User with tihs name already exists");
            if (db.Users.Any(u => u.Email == formuser.Email))
                return BadRequest("This email already used");

            User user = new User
            {
                Username = formuser.Username,
                Password = formuser.Password,
                Email = formuser.Email,
                Phone = formuser.Phone,
                Avatar = "Images/users/default.png"
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();

            // SendRegtstrationEmailAsync(user.Email, user.Username);

            return Ok();
        }

        [HttpGet]
        [Route("/reset")]
        public IActionResult Reset()
        {
            return View();
        }

        [HttpPut]
        [Route("/reset")]
        public async Task<IActionResult> Reset([FromForm] string email)
        {
            User? user = db.Users.FirstOrDefault(u => u.Email == email);
            if (user is null)
                return BadRequest();

            string password = RandomPassword();
            user.Password = password;

            db.Users.Update(user);
            await db.SaveChangesAsync();

            // SendNewPasswordAsync(user.Email, user.Username, password);

            return Ok();
        }

        private async void SendRegtstrationEmailAsync(string email, string username)
        {
            await emailSender.SendEmailAync(new EMessage
            {
                To = email,
                Subject = "Registration",
                Message = $"Wellcome to SELLER {username}"
            });
        }

        private async void SendNewPasswordAsync(string email, string username, string password)
        {  
            await emailSender.SendEmailAync(new EMessage
            {
                To = email,
                Subject = "New password",
                Message = $"Hello {username}" +
                          $"Your new password is - {password}"
            });
        }

        private static string RandomPassword()
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!$%";

            Random rnd = new Random();
            int passLength = rnd.Next(7, 13);

            StringBuilder res = new StringBuilder();
            for (int i = 0; i < passLength; i++)
            {
                res.Append(valid[rnd.Next(0, valid.Length)]);
            }

            return res.ToString();
        }
        
        private int GetUserId()
        {
            if (User.Claims.Any(c => c.Type == "Id"))
            {
                var claim = User.FindFirst("Id");
                if (claim is not null)
                    return int.Parse(claim.Value);
            }
            return -1;
        }
    }
}