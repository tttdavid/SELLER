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
    }
}