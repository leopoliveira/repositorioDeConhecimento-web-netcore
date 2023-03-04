using System.ComponentModel.DataAnnotations;

namespace RepositorioDeConhecimento.Models.Application.DTO
{
    public class ImagemDTO : BaseDto
    {
        [MaxLength(100)]
        public string Nome { get; set; }

        public byte[] Conteudo { get; set; }

        [MaxLength(10)]
        public string TipoArquivo { get; set; }

        public int TamanhoArquivo { get; set; }

        public int? ConhecimentoId { get; set; }

        public ConhecimentoDTO? Conhecimento { get; set; }
    }
}
