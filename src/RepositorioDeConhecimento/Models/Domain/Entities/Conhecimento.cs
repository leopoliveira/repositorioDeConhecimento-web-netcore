using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositorioDeConhecimento.Models.Domain.Entities
{
    public class Conhecimento : BaseEntity
    {
        public Conhecimento()
        {
            Tags = new List<Tag>();
            Imagens = new List<Imagem>();
        }

        [Required(ErrorMessage = "O campo {0} é obrigatório"), StringLength(50)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório"), Column(TypeName = "nvarchar(max)")]
        public string Conteudo { get; set; }

        [ForeignKey("Autor")]
        public int AutorId { get; set; }

        public Autor Autor { get; set; }

        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        [NotMapped]
        public ICollection<Imagem>? Imagens { get; set; }

        [NotMapped]
        public ICollection<Tag>? Tags { get; set; }

    }
}
