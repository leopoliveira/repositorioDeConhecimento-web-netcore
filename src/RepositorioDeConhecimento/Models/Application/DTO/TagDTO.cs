using System.ComponentModel.DataAnnotations;

namespace RepositorioDeConhecimento.Models.Application.DTO
{
    public class TagDTO : BaseDto
    {
        [MaxLength(50)]
        public string Nome { get; set; }

        public int ConhecimentoId { get; set; }

        public ConhecimentoDTO? Conhecimento { get; set; }
    }
}
