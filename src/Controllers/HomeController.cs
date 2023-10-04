using Microsoft.AspNetCore.Mvc;
using src.DataSource;

namespace src.Controllers
{
    public class HomeController : Controller
    {
        const int ITEMS_PER_PAGE = 24;
        const double IPPFC = (double)ITEMS_PER_PAGE;

        MainContext db;

        public HomeController(MainContext db)
        {
            this.db = db;
        }

        [Route("api/pagination/{keyword}/{pageId}")]
        public IActionResult PaginationGenre(string keyword, int pageId)
        {
            double xdd;
            if (keyword == "default")
                xdd = db.Products.Count() / IPPFC;
            else if (keyword.Contains('!'))
            {
                string genre = keyword.Substring(0, keyword.Length - 1);
                xdd = db.Products.Where(i => i.Title.Contains(keyword)).Count() / IPPFC;
            }
            else
            {
                xdd = db.Products.Where(p => p.Title.Contains(keyword) || p.Description.Contains(keyword)).Count() / IPPFC;
            }

            int lastPage = (int)Math.Ceiling(xdd);

            List<int> pages = GenNation(pageId, lastPage);
            return Json(pages);
        }

        [Route("api/{keyword}/{id}")]
        public IActionResult IndexGenre(string keyword, int id)
        {
            if (keyword == "default")
            {
                var result = db.Products.Skip(id * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();
                return Json(result);
            }
            else if (keyword.Contains('!'))
            {
                string genre = keyword.Substring(0, keyword.Length - 1).ToLower();
                var result = db.Products.Where(p => p.Type.Contains(genre)).Skip(id * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();
                return Json(result);
            }
            else
            {
                var result = db.Products.Where(p => p.Title.Contains(keyword) || p.Description.Contains(keyword)).Skip(id * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList(); ;
                return Json(result);
            }
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