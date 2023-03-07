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

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Conhecimento}/{action=Index}/{id?}");

            app.Run();
        }
    }
}