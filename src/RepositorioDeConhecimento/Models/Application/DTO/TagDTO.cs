namespace RepositorioDeConhecimento.Models.Application.DTO
{
    public class TagDTO : BaseDto
    {
        public string Nome { get; set; }

        public int ConhecimentoId { get; set; }

        public ConhecimentoDTO Conhecimento { get; set; }
    }
}
