using Microsoft.EntityFrameworkCore;
using RepositorioDeConhecimento.Models.Domain.Entities;

namespace RepositorioDeConhecimento.Infrastructure
{
    public class AppDbContext : DbContext
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
