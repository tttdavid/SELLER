using System.Data;
using Microsoft.AspNetCore.Mvc;
using src.DataSource;
using src.ViewModels;
using src.Models;

namespace src.Controllers
{
    public class UserController : Controller
    {
        const int ITEMS_PER_PAGE = 24;
        const double IPPFC = (double)ITEMS_PER_PAGE;
        MainContext db;

        public UserController(MainContext db)
        {
            this.db = db;
        }

        [Route("/user/pagination/{userId}/{pageId}")]
        public IActionResult UserNation(int userId, int pageId)
        {
            double xdd = db.Products.Where(p => p.AuthorId == userId).Count() / IPPFC;
            int lastPage = (int)Math.Ceiling(xdd);

            List<int> pages = GenNation(pageId, lastPage);
            return Json(pages);
        }

        [Route("user/{userId}/{pageId}")]
        public IActionResult UserPdoducts(int userId, int pageId)
        {
            User? user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user is null)
                return NotFound();

            List<Product> result = db.Products.Where(p => p.AuthorId == userId).ToList().Skip(pageId * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();
            return Json(result);
        }

        [Route("/user/{userId}")]
        public IActionResult Index(int userId)
        {
            User? user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user is null)
                return NotFound();

            UserView model = new UserView
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                Avatar = user.Avatar
            };

            return View(model);
        }

        private List<int> GenNation(int pageId, int lastPage)
        {
            List<int> list = new List<int>();
            if (lastPage < 3)
            {
                for (int i = 0; i < lastPage; i++)
                    list.Add(i);
                return list;
            }

            if (pageId == 0)
            {
                list.Add(0);
                list.Add(1);
                list.Add(2);
            }
            else if (pageId + 1 == lastPage)
            {
                list.Add(lastPage - 3);
                list.Add(lastPage - 2);
                list.Add(lastPage - 1);
            }
            else
            {
                list.Add(pageId - 1);
                list.Add(pageId);
                list.Add(pageId + 1);
            }
            return list;
        }
    }
}