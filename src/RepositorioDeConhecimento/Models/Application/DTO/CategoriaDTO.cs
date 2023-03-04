using System.ComponentModel.DataAnnotations;

namespace RepositorioDeConhecimento.Models.Application.DTO
{
    public class CategoriaDTO : BaseDto
    {
        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(200)]
        public string? Descricao { get; set; }

        public int? IconeId { get; set; }

        public ImagemDTO? Icone { get; set; }
    }
}
