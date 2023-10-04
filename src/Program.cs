using src.DataSource;
using Email;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            DotNetEnv.Env.Load("../build/");
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                WebRootPath = "Static"
            });

            builder.Services.AddMvc();
            builder.Services.AddMemoryCache();
            builder.Services.AddDbContext<MainContext>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/login");
            builder.Services.AddTransient<IEmailSender, EmailSender>();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}