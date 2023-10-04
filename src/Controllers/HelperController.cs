using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DataSource;

namespace src.Controllers
{
    public class HelperController : Controller
    {
        MainContext db;

        public HelperController(MainContext db)
        {
            this.db = db;
        }

        [AcceptVerbs("Get", "Post")]
        [Route("/CheckUseraname")]
        public IActionResult CheckUseraname(string username)
        {
            bool isUsernameTaken = db.Users.Any(u => u.Username == username);
            return Json(!isUsernameTaken);
        }

        [AcceptVerbs("Get", "Post")]
        [Route("CheckEmail")]
        public IActionResult CheckEmail(string email)
        {
            bool isEmailTaken = db.Users.Any(u => u.Email == email);
            return Json(!isEmailTaken);
        }

        [Route("/xdd")]
        public IActionResult Xdd()
        {
            int id = GetUserId();
            for (int i = 0; i <= 30; i++)
            {
                db.Products.Add(new Models.Product
                {
                    Title = $"Product {i + 1}",
                    Description = $"Product {i + 1} description",
                    Type = "service",
                    Price = i * 2 + 10,
                    Image = "/Images/Products/Default.png",
                    AuthorId = id
                });
            }

            db.SaveChanges();
            return Content("Done");
            
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