namespace RepositorioDeConhecimento.Models.Application.DTO
{
    public class ImagemDTO : BaseDto
    {
        public string Nome { get; set; }

        public byte[] Conteudo { get; set; }

        public string TipoArquivo { get; set; }

        public int TamanhoArquivo { get; set; }

        public int? ConhecimentoId { get; set; }

        public ConhecimentoDTO Conhecimento { get; set; }
    }
}
