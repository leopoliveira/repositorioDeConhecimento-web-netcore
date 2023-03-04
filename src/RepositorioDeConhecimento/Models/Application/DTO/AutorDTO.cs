using System.ComponentModel.DataAnnotations;

namespace RepositorioDeConhecimento.Models.Application.DTO
{
    public class AutorDTO : BaseDto
    {
        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [MaxLength(100)]
        [DataType(DataType.PhoneNumber)]
        public string? Telefone { get; set; }

        [MaxLength(6)]
        public string Sigla { get; set; }

        public int? FotoId { get; set; }

        public ImagemDTO? Foto { get; set; }
    }
}
