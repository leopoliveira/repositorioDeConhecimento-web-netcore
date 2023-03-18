using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class ConhecimentoController : BaseController
    {
        private readonly IConhecimentoRepository _repository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IAutorRepository _autorRepository;
        private readonly IMapper _mapper;

        public ConhecimentoController(IConhecimentoRepository conhecimentoRepository, ICategoriaRepository categoriaRepository, IAutorRepository autorRepository, IMapper mapper, UserManager<IdentityUser<int>> userManager) : base(userManager)
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
            int usuarioId = base.GetUserId();

            IEnumerable<Conhecimento> conhecimentos;

            if (!string.IsNullOrEmpty(searchTerm) || !string.IsNullOrWhiteSpace(searchTerm))
            {
                conhecimentos = await _repository.GetWhere(c => c.IdUsuario == usuarioId &&
                                                        (c.Titulo.Contains(searchTerm) ||
                                                        c.Conteudo.Contains(searchTerm) ||
                                                        c.Autor.Nome.Contains(searchTerm) ||
                                                        c.Categoria.Nome.Contains(searchTerm)));

                if (conhecimentos.Any())
                {
                    ViewBag.TotalOfPages = 0;
                    ViewBag.CurrentPage = 0;

                    return View(ConvertConhecimentoToDto(conhecimentos));
                }
            }

            conhecimentos = await _repository.GetByPages(usuarioId, page, offset, numberOfRecords);

            int totalOfRecords = await _repository.CountRecords(usuarioId);

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
            int usuarioId = base.GetUserId();

            Conhecimento conhecimento = await _repository.GetById(id, usuarioId);

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
                conhecimento.IdUsuario = base.GetUserId();
                
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
            int usuarioId = base.GetUserId();

            if (id <= 0)
            {
                TempData["message"] = Message.CreateMessage("Erro. Informe um Id válido", MessageType.Error);
                return View("Index");
            }

            Conhecimento conhecimento = await _repository.GetById(id, usuarioId);

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

        /// <summary>
        /// Clona um Conhecimento.
        /// </summary>
        /// <param name="id">Id do Conhecimento a ser clonado.</param>
        /// <returns>A View com o registro a ser salvo.</returns>
        [HttpGet]
        public async Task<IActionResult> Clonar(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("GetConhecimento", new { id = 0 });
            }

            int usuarioId = base.GetUserId();

            Conhecimento conhecimentoAtual = await _repository.GetById(id, usuarioId);

            if (conhecimentoAtual == null)
            {
                return RedirectToAction("GetConhecimento", new { id = 0 });
            }

            Conhecimento conhecimentoClone = _mapper.Map<Conhecimento>(conhecimentoAtual);

            ConhecimentoViewModel viewModel = await ConvertToConhecimentoViewModel(_mapper.Map<ConhecimentoDTO>(conhecimentoClone));

            return View(viewModel);
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
            int usuarioId = base.GetUserId();

            ICollection<Categoria> categorias = await _categoriaRepository.GetAll(usuarioId);

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
            int usuarioId = base.GetUserId();

            ICollection<Autor> autores = await _autorRepository.GetAll(usuarioId);

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
