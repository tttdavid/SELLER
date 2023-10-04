using src.Models;

namespace src.ViewModels
{
    public class ProductWithAuthor
    {
        public Product? Product { set; get; }
        public UserView? User { set; get; }
        public bool IsAuthor { get; set; }
    }
}