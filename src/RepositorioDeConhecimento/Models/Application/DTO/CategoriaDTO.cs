namespace RepositorioDeConhecimento.Models.Application.DTO
{
    public class CategoriaDTO : BaseDto
    {
        public string Nome { get; set; }

        public string? Descricao { get; set; }

        public int? IconeId { get; set; }

        public ImagemDTO Icone { get; set; }
    }
}
