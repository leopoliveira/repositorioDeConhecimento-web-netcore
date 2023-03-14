using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepositorioDeConhecimento.Infrastructure.Helpers.Messages;
using RepositorioDeConhecimento.Models.Application.DTO;
using RepositorioDeConhecimento.Models.Domain.Entities;
using RepositorioDeConhecimento.Models.Domain.Repositories;
using RepositorioDeConhecimento.Models.ViewModels;
using System.Collections;

namespace RepositorioDeConhecimento.Controllers
{
    [Authorize]
    public class ConhecimentoController : Controller
    {
        private readonly IConhecimentoRepository _repository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IAutorRepository _autorRepository;
        private readonly IMapper _mapper;

        public ConhecimentoController(IConhecimentoRepository conhecimentoRepository, ICategoriaRepository categoriaRepository, IAutorRepository autorRepository, IMapper mapper)
        {
            _repository = conhecimentoRepository;
            _categoriaRepository = categoriaRepository;
            _autorRepository = autorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all saved Conhecimentos.
        /// </summary>
        /// <returns>Conhecinentos</returns>
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int offset = 20, int numberOfRecords = 20, string searchTerm = "")
        {
            IEnumerable<Conhecimento> conhecimentos;

            if (!string.IsNullOrEmpty(searchTerm) || !string.IsNullOrWhiteSpace(searchTerm))
            {
                conhecimentos = await _repository.GetWhere(c =>
                                                        c.Titulo.Contains(searchTerm) ||
                                                        c.Conteudo.Contains(searchTerm) ||
                                                        c.Autor.Nome.Contains(searchTerm) ||
                                                        c.Categoria.Nome.Contains(searchTerm));

                if (conhecimentos.Any())
                {
                    ViewBag.TotalOfPages = 0;
                    ViewBag.CurrentPage = 0;

                    return View(ConvertConhecimentoToDto(conhecimentos));
                }
            }

            conhecimentos = await _repository.GetByPages(page, offset, numberOfRecords);

            int totalOfRecords = await _repository.CountRecords();

            ViewBag.TotalOfPages = Math.Ceiling(Convert.ToDecimal(totalOfRecords) / offset);
            ViewBag.CurrentPage = page;

            return View(ConvertConhecimentoToDto(conhecimentos));
        }

        /// <summary>
        /// Gets a specific Conhecimento by Id.
        /// </summary>
        /// <returns>That specific Conhecimento</returns>
        [HttpGet]
        public async Task<IActionResult> GetConhecimento(int id)
        {
            Conhecimento conhecimento = await _repository.GetById(id);

            ConhecimentoDTO dto = _mapper.Map<ConhecimentoDTO>(conhecimento) ?? new ConhecimentoDTO();

            ConhecimentoViewModel viewModel = await ConvertToConhecimentoViewModel(dto);

            return View("Save", viewModel);
        }

        /// <summary>
        /// Save a Conhecimento.
        /// </summary>
        /// <param name="dto">An object of ConhecimentoDTO type.</param>
        /// <returns>View Index.</returns>
        [HttpPost]
        public async Task<IActionResult> Save(ConhecimentoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["message"] = Message.CreateMessage("Dados inválidos. Por favor, revise os dados inseridos", MessageType.Error);

                viewModel.Categorias = await GetCategorias();
                viewModel.Autores = await GetAutores();

                return View(viewModel);
            }

            Conhecimento conhecimento = ConvertFromConhecimentoViewModel(viewModel);

            if (conhecimento.Id > 0)
            {
                await _repository.Update(conhecimento);
            }
            else
            {
                await _repository.Insert(conhecimento);
            }


            TempData["message"] = Message.CreateMessage("Dados salvos com sucesso!", MessageType.Success);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Delete a Conhecimento.
        /// </summary>
        /// <param name="id">Conhecimento Id.</param>
        /// <returns>View Delete.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData["message"] = Message.CreateMessage("Erro. Informe um Id válido", MessageType.Error);
                return View("Index");
            }

            Conhecimento conhecimento = await _repository.GetById(id);

            if (conhecimento == null)
            {
                TempData["message"] = Message.CreateMessage("Erro. Informe um Id válido", MessageType.Error);
                return View("Index");
            }

            ConhecimentoDTO categoriaDTO = _mapper.Map<ConhecimentoDTO>(conhecimento);

            return View("Delete", categoriaDTO);
        }

        /// <summary>
        /// Delete a Conhecimento.
        /// </summary>
        /// <param name="dto">An object of ConhecimentoDTO type.</param>
        /// <returns>View Index.</returns>
        [HttpPost]
        public async Task<IActionResult> Delete(ConhecimentoDTO dto)
        {
            if (dto.Id <= 0)
            {
                TempData["message"] = Message.CreateMessage("Erro. Informe um Id válido", MessageType.Error);
                return View(dto);
            }

            Conhecimento conhecimento = _mapper.Map<Conhecimento>(dto);

            await _repository.Delete(conhecimento);

            TempData["message"] = Message.CreateMessage("Dados excluídos com sucesso!", MessageType.Success);

            return RedirectToAction("Index");
        }

        private ICollection<ConhecimentoDTO> ConvertConhecimentoToDto(IEnumerable<Conhecimento> conhecimentos)
        {
            ICollection<ConhecimentoDTO> dtoConhecimentos = new List<ConhecimentoDTO>();

            foreach (Conhecimento conhecimento in conhecimentos)
            {
                ConhecimentoDTO dto = _mapper.Map<ConhecimentoDTO>(conhecimento);

                dtoConhecimentos.Add(dto);
            }

            return dtoConhecimentos;
        }

        private async Task<ConhecimentoViewModel> ConvertToConhecimentoViewModel(ConhecimentoDTO conhecimentoDTO)
        {
            ConhecimentoViewModel viewModel = new ConhecimentoViewModel();

            viewModel.Conhecimento = conhecimentoDTO;
            viewModel.Categorias = await GetCategorias();
            viewModel.Autores = await GetAutores();

            return viewModel;
        }

        private Conhecimento ConvertFromConhecimentoViewModel(ConhecimentoViewModel conhecimentoViewModel)
        {
            Conhecimento conhecimento = new Conhecimento();

            conhecimento.Id = conhecimentoViewModel.Conhecimento.Id;
            conhecimento.Titulo = conhecimentoViewModel.Conhecimento.Titulo;
            conhecimento.Conteudo = conhecimentoViewModel.Conhecimento.Conteudo;
            conhecimento.CategoriaId = conhecimentoViewModel.Conhecimento.CategoriaId;
            conhecimento.AutorId = conhecimentoViewModel.Conhecimento.AutorId;

            return conhecimento;

        }

        private async Task<ICollection<CategoriaDTO>> GetCategorias()
        {
            ICollection<Categoria> categorias = await _categoriaRepository.GetAll();

            ICollection<CategoriaDTO> categoriasDTO = new List<CategoriaDTO>();

            if (categorias != null)
            {
                foreach (Categoria categoria in categorias)
                {
                    categoriasDTO.Add(_mapper.Map<CategoriaDTO>(categoria));
                }
            }

            return categoriasDTO;
        }

        private async Task<ICollection<AutorDTO>> GetAutores()
        {
            ICollection<Autor> autores = await _autorRepository.GetAll();

            ICollection<AutorDTO> autoresDTO = new List<AutorDTO>();

            if (autores != null)
            {
                foreach (Autor autor in autores)
                {
                    autoresDTO.Add(_mapper.Map<AutorDTO>(autor));
                }
            }

            return autoresDTO;
        }
    }
}
