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
using src.ViewModels;

namespace src.Controllers
{
    public class PersonalController : Controller
    {
        MainContext db;
        IEmailSender emailSender;
        IMemoryCache cache;
        public PersonalController(MainContext db, IEmailSender emailSender, IMemoryCache cache)
        {
            this.db = db;
            this.emailSender = emailSender;
            this.cache = cache;
        }

        [HttpGet]
        [Authorize]
        [Route("/profile")]
        public IActionResult Index()
        {
            int userId = GetUserId();
            User? user = null;
            if (cache.TryGetValue(userId, out User? dummy))
                user = dummy;
            else
                user = db.Users.FirstOrDefault(u => u.Id == userId);

            if (user is null)
                return BadRequest();

            UserView model = new UserView
            {
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                Avatar = user.Avatar
            };

            return View(model);
        }

        [HttpPost]
        [Route("/profile/editavatar")]
        public async Task<IActionResult> EditProfilePicture([FromForm] IFormFile? file)
        {
            int userId = GetUserId();
            User? user = null;
            if (cache.TryGetValue(userId, out User? dummy))
                user = dummy;
            else
                user = db.Users.FirstOrDefault(u => u.Id == userId);

            if (user is null)
                return BadRequest();

            if (file is null)
                return BadRequest("Bad image");

            string filename = Guid.NewGuid().ToString() + file.FileName;
            string dbpath = $"/Images/Users/{filename}";
            string filePath = Path.Combine("Static/Images/Users/", filename);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            if (user.Avatar is not null)
                DeleteOldAvatar(user.Avatar);

            user.Avatar = dbpath;

            db.Users.Update(user);
            db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("/profile/editusername")]
        public IActionResult EditUsername([FromForm] string username)
        {
            int userId = GetUserId();
            User? user = null;
            if (cache.TryGetValue(userId, out User? dummy))
                user = dummy;
            else
                user = db.Users.FirstOrDefault(u => u.Id == userId);

            if (user is null || db.Users.Any(u => u.Username == username))
                return BadRequest("User with this name already exsits");

            user.Username = username;

            db.Users.Update(user);
            db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("/profile/editphone")]
        public IActionResult EditPhone([FromForm] string phone)
        {
            int userId = GetUserId();
            User? user = null;
            if (cache.TryGetValue(userId, out User? dummy))
                user = dummy;
            else
                user = db.Users.FirstOrDefault(u => u.Id == userId);

            if (user is null)
                return BadRequest("User is null");

            user.Phone = phone;

            db.Users.Update(user);
            db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("/profile/editemail")]
        public IActionResult EditEmail([FromForm] string email)
        {
            int userId = GetUserId();
            User? user = null;
            if (cache.TryGetValue(userId, out User? dummy))
                user = dummy;
            else
                user = db.Users.FirstOrDefault(u => u.Id == userId);

            if (user is null || db.Users.Any(u => u.Email == email))
                return BadRequest("Email already taken");

            user.Email = email;

            db.Users.Update(user);
            db.SaveChanges();

            // Disabled cuz outlook things 
            //EmailChanged(user.Email, user.Username);

            return Ok();
        }

        [HttpPost]
        [Route("/profile/editpassword")]
        public IActionResult EditPassword([FromForm] string oldpass, string newpass)
        {
            int userId = GetUserId();
            User? user = null;
            if (cache.TryGetValue(userId, out User? dummy))
                user = dummy;
            else
                user = db.Users.FirstOrDefault(u => u.Id == userId);

            if (user is null)
                return BadRequest("User not found");

            if (user.Password != oldpass)
                return BadRequest("Wrong old password");

            user.Password = newpass;

            db.Users.Update(user);
            db.SaveChanges();

            return Ok();
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

        static void DeleteOldAvatar(string avatar)
        {
            string filename = avatar[14..];
            string path = Path.Combine("Static/Images/Users/", filename);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        private async void EmailChanged(string email, string username)
        {
            await emailSender.SendEmailAync(new EMessage
            {
                To = email,
                Subject = "Email changed",
                Message = $"Hello {username}! now this is your email at SELLER"
            });
        }
    }
}