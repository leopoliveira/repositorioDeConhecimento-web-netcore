using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepositorioDeConhecimento.Infrastructure.Helpers.Messages;
using RepositorioDeConhecimento.Models.Application.DTO;
using RepositorioDeConhecimento.Models.Domain.Entities;
using RepositorioDeConhecimento.Models.Domain.Repositories;
using System.Xml;

namespace RepositorioDeConhecimento.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all saved Categorias.
        /// </summary>
        /// <returns>Categorias</returns>
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int offset = 10, int numberOfRecords = 10)
        {
            IEnumerable<Categoria> categorias = await _repository.GetByPages(page, offset, numberOfRecords);

            ICollection<CategoriaDTO> dtoCategorias = new List<CategoriaDTO>();

            foreach (Categoria conhecimento in categorias)
            {
                CategoriaDTO dto = _mapper.Map<CategoriaDTO>(conhecimento);

                dtoCategorias.Add(dto);
            }

            int totalOfRecords = await _repository.CountRecords();

            ViewBag.TotalOfPages = Math.Ceiling(Convert.ToDecimal(totalOfRecords) / offset);
            ViewBag.CurrentPage = page;

            return View(dtoCategorias);
        }

        /// <summary>
        /// Gets a specific Categoria by Id.
        /// </summary>
        /// <returns>That specific Categoria</returns>
        [HttpGet]
        public async Task<IActionResult> GetCategoria(int id)
        {
            Categoria conhecimento = await _repository.GetById(id);

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

            Categoria conhecimento = _mapper.Map<Categoria>(dto);

            if(conhecimento.Id > 0)
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
        /// Delete a Categoria.
        /// </summary>
        /// <param name="id">Categoria Id.</param>
        /// <returns>View Delete.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData["message"] = Message.CreateMessage("Erro. Informe um Id válido", MessageType.Error);
                return View("Index");
            }

            Categoria conhecimento = await _repository.GetById(id);

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
    }
}
