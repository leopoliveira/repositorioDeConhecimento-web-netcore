using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RepositorioDeConhecimento.Models.Domain.Entities;

namespace RepositorioDeConhecimento.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Conhecimento> Conhecimentos { get; set; }

        public DbSet<Imagem> Imagens { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}
