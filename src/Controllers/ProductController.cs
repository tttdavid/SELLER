using System.Data;
using Microsoft.AspNetCore.Mvc;
using src.DataSource;
using src.ViewModels;
using src.Models;
using Microsoft.AspNetCore.Authorization;

namespace src.Controllers
{
    public class ProductController : Controller
    {
        MainContext db;
        public ProductController(MainContext db)
        {
            this.db = db;
        }

        [HttpDelete]
        [Route("/product/{id}")]
        public IActionResult DeleteWithId(int id)
        {

            Product? product = db.Products.FirstOrDefault(p => p.Id == id);
            if (product is null)
                return BadRequest();

            if (product.Image is not null)
                DeleteOldProductImage(product.Image);

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("/product/{id}")]
        public IActionResult Index(int id)
        {
            Product? product = db.Products.FirstOrDefault(p => p.Id == id);
            if (product is null)
                return NotFound();
            User? user = db.Users.FirstOrDefault(u => u.Id == product.AuthorId);
            if (user is null)
                return NotFound();

            int userId = -1;

            if (User.Claims.Any(c => c.Type == "Id"))
            {
                var claim = User.FindFirst("Id");
                if (claim is not null)
                    userId = int.Parse(claim.Value);
            }

            ProductWithAuthor model = new ProductWithAuthor
            {
                Product = db.Products.FirstOrDefault(p => p.Id == id),
                User = new UserView
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Phone = user.Phone,
                    Avatar = user.Avatar
                },
                IsAuthor = userId == user.Id
            };
            return View(model);
        }

        [HttpGet]
        [Route("/addproduct")]
        [Authorize]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [Route("/addproduct")]
        public async Task<IActionResult> AddProduct([FromForm] AddProduct addProduct)
        {
            if (!ModelState.IsValid)
                return BadRequest("Bad data");

            IFormFile? file = addProduct.Image;
            if (file is null)
                return BadRequest("Bad image");

            string filename = Guid.NewGuid().ToString() + file.FileName;
            string dbpath = $"/Images/Products/{filename}";
            string filePath = Path.Combine("Static/Images/Products", filename);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            int id = -1;

            if (User.Claims.Any(c => c.Type == "Id"))
            {
                var claim = User.FindFirst("Id");
                if (claim is not null)
                    id = int.Parse(claim.Value);
            }
            else
            {
                id = db.Products.Count() + 1;
            }

            Product product = new Product
            {
                Title = addProduct.Title,
                Description = addProduct.Description,
                Type = addProduct.Type,
                Price = addProduct.Price,
                Image = dbpath,
                AuthorId = id
            };

            db.Products.Add(product);
            db.SaveChanges();

            return Ok();
        }

        static void DeleteOldProductImage(string image)
        {
            Console.WriteLine("start");
            string filename = image[17..];
            string path = Path.Combine("Static/Images/Products/", filename);
            if (System.IO.File.Exists(path))
            {
                Console.WriteLine("exists");
                System.IO.File.Delete(path);
            }
            Console.WriteLine("done");
        }
    }
}