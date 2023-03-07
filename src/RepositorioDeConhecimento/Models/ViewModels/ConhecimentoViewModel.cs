using RepositorioDeConhecimento.Models.Application.DTO;

namespace RepositorioDeConhecimento.Models.ViewModels
{
    public class ConhecimentoViewModel
    {
        public ConhecimentoDTO Conhecimento { get; set; } = new ConhecimentoDTO();

        public ICollection<CategoriaDTO> Categorias { get; set; } = new List<CategoriaDTO>();

        public ICollection<AutorDTO> Autores { get; set; } = new List<AutorDTO>();
    }
}
