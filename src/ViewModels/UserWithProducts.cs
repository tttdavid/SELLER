using src.Models;

namespace src.ViewModels
{
    public class UserWithProducts
    {
        public UserView? User { get; set; }
        public List<Product>? Products { get; set; }
    }
}