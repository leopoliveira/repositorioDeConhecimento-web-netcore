using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositorioDeConhecimento.Models.Domain.Entities
{
    public class Autor : BaseEntity
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório"), StringLength(100)]
        public string Nome { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? Telefone { get; set; }

        [StringLength(6)]
        public string Sigla { get; set; }

        public int? FotoId { get; set; }

        [NotMapped]
        public Imagem Foto { get; set; }
    }
}
