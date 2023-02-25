using System.ComponentModel.DataAnnotations;

namespace RepositorioDeConhecimento.Models.Application.DTO
{
    public class AutorDTO : BaseDto
    {
        public string Nome { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? Telefone { get; set; }

        public string Sigla { get; set; }

        public int? FotoId { get; set; }

        public ImagemDTO Foto { get; set; }
    }
}
