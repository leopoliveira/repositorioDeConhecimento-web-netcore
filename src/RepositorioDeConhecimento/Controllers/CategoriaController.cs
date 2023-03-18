using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepositorioDeConhecimento.Infrastructure.Helpers.Messages;
using RepositorioDeConhecimento.Models.Application.DTO;
using RepositorioDeConhecimento.Models.Domain.Entities;
using RepositorioDeConhecimento.Models.Domain.Repositories;
using System.Xml;

namespace RepositorioDeConhecimento.Controllers
{
    [Authorize]
    public class CategoriaController : BaseController
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaRepository repository, IMapper mapper, UserManager<IdentityUser<int>> userManager) : base(userManager)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all saved Categorias.
        /// </summary>
        /// <returns>Categorias</returns>
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int offset = 10, int numberOfRecords = 10, string searchTerm = "")
        {
            int usuarioId = base.GetUserId();

            IEnumerable<Categoria> categorias;

            if (!string.IsNullOrEmpty(searchTerm) || !string.IsNullOrWhiteSpace(searchTerm))
            {
                categorias = await _repository.GetWhere(c => c.Id == usuarioId &&
                                                        (c.Nome.Contains(searchTerm) ||
                                                        c.Descricao.Contains(searchTerm)));

                ViewBag.TotalOfPages = 0;
                ViewBag.CurrentPage = 0;

                return View(ConvertCategoriaToDto(categorias));
            }

            categorias = await _repository.GetByPages(usuarioId, page, offset, numberOfRecords);

            int totalOfRecords = await _repository.CountRecords(usuarioId);

            ViewBag.TotalOfPages = Math.Ceiling(Convert.ToDecimal(totalOfRecords) / offset);
            ViewBag.CurrentPage = page;

            return View(ConvertCategoriaToDto(categorias));
        }

        /// <summary>
        /// Gets a specific Categoria by Id.
        /// </summary>
        /// <returns>That specific Categoria</returns>
        [HttpGet]
        public async Task<IActionResult> GetCategoria(int id)
        {
            int usuarioId = base.GetUserId();

            Categoria conhecimento = await _repository.GetById(id, usuarioId);

            CategoriaDTO dto =  _mapper.Map<CategoriaDTO>(conhecimento) ?? new CategoriaDTO();

            return View("Save", dto);
        }

        /// <summary>
        /// Save a Categoria.
        /// </summary>
        /// <param name="dto">An object of CategoriaDTO type.</param>
        /// <returns>View Index.</returns>
        [HttpPost]
        public async Task<IActionResult> Save(CategoriaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["message"] =  Message.CreateMessage("Dados inválidos. Por favor, revise os dados inseridos", MessageType.Error);

                return View(dto);
            }

            dto.IconeId ??= 0;

            Categoria categoria = _mapper.Map<Categoria>(dto);

            if(categoria.Id > 0)
            {
                await _repository.Update(categoria);
            }
            else
            {
                categoria.IdUsuario = base.GetUserId();

                await _repository.Insert(categoria);
            }
            

            TempData["message"] = Message.CreateMessage("Dados salvos com sucesso!", MessageType.Success);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Delete a Categoria.
        /// </summary>
        /// <param name="id">Categoria Id.</param>
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

            Categoria conhecimento = await _repository.GetById(id, usuarioId);

            if (conhecimento == null)
            {
                TempData["message"] = Message.CreateMessage("Erro. Informe um Id válido", MessageType.Error);
                return View("Index");
            }

            CategoriaDTO categoriaDTO = _mapper.Map<CategoriaDTO>(conhecimento);

            return View("Delete", categoriaDTO);
        }

        /// <summary>
        /// Delete a Categoria.
        /// </summary>
        /// <param name="dto">An object of CategoriaDTO type.</param>
        /// <returns>View Index.</returns>
        [HttpPost]
        public async Task<IActionResult> Delete(CategoriaDTO dto)
        {
            if (dto.Id <= 0)
            {
                TempData["message"] = Message.CreateMessage("Erro. Informe um Id válido", MessageType.Error);
                return View(dto);
            }

            Categoria conhecimento = _mapper.Map<Categoria>(dto);

            await _repository.Delete(conhecimento);

            TempData["message"] = Message.CreateMessage("Dados excluídos com sucesso!", MessageType.Success);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Clona uma Categoria.
        /// </summary>
        /// <param name="id">Id da Categoria a ser clonada.</param>
        /// <returns>A View com o registro a ser salvo.</returns>
        [HttpGet]
        public async Task<IActionResult> Clonar(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("GetCategoria", new { id = 0 });
            }

            int usuarioId = base.GetUserId();

            Categoria categoriaAtual = await _repository.GetById(id, usuarioId); 

            if (categoriaAtual == null)
            {
                return RedirectToAction("GetCategoria", new { id = 0 });
            }

            Categoria categoriaClone = _mapper.Map<Categoria>(categoriaAtual);

            categoriaClone.Id = 0;

            CategoriaDTO dto = _mapper.Map<CategoriaDTO>(categoriaClone);

            return View(dto);
        }

        private ICollection<CategoriaDTO> ConvertCategoriaToDto(IEnumerable<Categoria> categorias)
        {
            ICollection<CategoriaDTO> dtoCategorias = new List<CategoriaDTO>();

            foreach (Categoria conhecimento in categorias)
            {
                CategoriaDTO dto = _mapper.Map<CategoriaDTO>(conhecimento);

                dtoCategorias.Add(dto);
            }

            return dtoCategorias;
        }
    }
}
