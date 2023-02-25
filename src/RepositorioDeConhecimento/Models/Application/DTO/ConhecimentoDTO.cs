namespace RepositorioDeConhecimento.Models.Application.DTO
{
    public class ConhecimentoDTO : BaseDto
    {
        public ConhecimentoDTO()
        {
            Tags = new List<TagDTO>();
            Imagens = new List<ImagemDTO>();
        }

        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public int AutorId { get; set; }

        public AutorDTO Autor { get; set; }

        public int CategoriaId { get; set; }

        public CategoriaDTO Categoria { get; set; }

        public ICollection<ImagemDTO>? Imagens { get; set; }

        public ICollection<TagDTO>? Tags { get; set; }
    }
}
