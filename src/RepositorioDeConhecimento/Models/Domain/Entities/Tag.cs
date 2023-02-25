using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositorioDeConhecimento.Models.Domain.Entities
{
    public class Tag : BaseEntity
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório"), StringLength(50)]
        public string Nome { get; set; }

        [ForeignKey("Conhecimento")]
        public int ConhecimentoId { get; set; }

        public Conhecimento Conhecimento { get; set; }
    }
}
