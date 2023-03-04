using RepositorioDeConhecimento.Models.Application.DTO;

namespace RepositorioDeConhecimento.Models.Application.ViewModels
{
    public class RepositorioViewModel
    {
        public IEnumerable<AutorDTO> Autores { get; set; }

        public IEnumerable<CategoriaDTO> Categorias { get; set; }

        public ConhecimentoDTO Conhecimento { get; set; }

        public IEnumerable<ImagemDTO> Imagens { get; set; }

        public IEnumerable<TagDTO> Tags { get; set; }

    }
}
