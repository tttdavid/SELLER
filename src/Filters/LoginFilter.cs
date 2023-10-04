using Microsoft.AspNetCore.Mvc.Filters;

namespace src.Filters
{
    public class LoginFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        { }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // Generates cookie to read it in default.js and load navigation buttons based on result
            if (context.HttpContext.Response.StatusCode == 200)
            {
                context.HttpContext.Response.Cookies.Append("SKzgEs5BKE8f1%", "", new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            }
        }
    }
}