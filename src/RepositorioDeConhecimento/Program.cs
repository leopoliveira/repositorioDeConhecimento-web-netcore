using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositorioDeConhecimento.Infrastructure.Context;
using RepositorioDeConhecimento.Infrastructure.Repositories;
using RepositorioDeConhecimento.Models.Domain.Repositories;

namespace RepositorioDeConhecimento
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DevConnection");

            builder.Services.AddControllersWithViews();

            // Db Context
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Identity
            builder.Services.AddIdentity<IdentityUser<int>, IdentityRole<int>>()
                            .AddEntityFrameworkStores<AppDbContext>()
                            .AddDefaultTokenProviders();

            // Identity Configuration
            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password Settings
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                // Lockout Settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;


                // User Settings
                options.User.RequireUniqueEmail = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Cookies Settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromMinutes(5);

                options.SlidingExpiration = true;
            });

            // Automapper DI
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Repositories DI
            builder.Services.AddScoped<IAutorRepository, AutorRepository>();
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddScoped<IConhecimentoRepository, ConhecimentoRepository>();
            builder.Services.AddScoped<IImagemRepository,  ImagemRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Conhecimento/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Conhecimento}/{action=Index}/{id?}");

            app.Run();
        }
    }
}