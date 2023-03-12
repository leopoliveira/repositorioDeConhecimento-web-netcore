using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepositorioDeConhecimento.Infrastructure.Helpers.Messages;
using RepositorioDeConhecimento.Models.Application.DTO;
using RepositorioDeConhecimento.Models.Domain.Entities;
using RepositorioDeConhecimento.Models.Domain.Repositories;

namespace RepositorioDeConhecimento.Controllers
{
    public class AutorController : Controller
    {
        private readonly IAutorRepository _repository;
        private readonly IMapper _mapper;

        public AutorController(IAutorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all saved Autores.
        /// </summary>
        /// <returns>Autores</returns>
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int offset = 10, int numberOfRecords = 10)
        {
            IEnumerable<Autor> autores = await _repository.GetByPages(page, offset, numberOfRecords);

            ICollection<AutorDTO> dtoAutores = new List<AutorDTO>();

            foreach (Autor autor in autores)
            {
                AutorDTO dto = _mapper.Map<AutorDTO>(autor);

                dtoAutores.Add(dto);
            }

            int totalOfRecords = await _repository.CountRecords();

            ViewBag.TotalOfPages = Math.Ceiling(Convert.ToDecimal(totalOfRecords) / offset);
            ViewBag.CurrentPage = page;

            return View(dtoAutores);
        }

        /// <summary>
        /// Gets a specific Autor by Id.
        /// </summary>
        /// <returns>That specific Autor</returns>
        [HttpGet]
        public async Task<IActionResult> GetAutor(int id)
        {
            Autor autor = await _repository.GetById(id);

            AutorDTO dto = _mapper.Map<AutorDTO>(autor) ?? new AutorDTO();

            return View("Save", dto);
        }

        /// <summary>
        /// Save a Autor.
        /// </summary>
        /// <param name="dto">An object of AutorDTO type.</param>
        /// <returns>View Index.</returns>
        [HttpPost]
        public async Task<IActionResult> Save(AutorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["message"] = Message.CreateMessage("Dados inválidos. Por favor, revise os dados inseridos", MessageType.Error);

                return View(dto);
            }

            dto.FotoId ??= 0;

            Autor autor = _mapper.Map<Autor>(dto);

            if (autor.Id > 0)
            {
                await _repository.Update(autor);
            }
            else
            {
                await _repository.Insert(autor);
            }


            TempData["message"] = Message.CreateMessage("Dados salvos com sucesso!", MessageType.Success);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Delete a Autor.
        /// </summary>
        /// <param name="id">Autor Id.</param>
        /// <returns>View Delete.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData["message"] = Message.CreateMessage("Erro. Informe um Id válido", MessageType.Error);
                return View("Index");
            }

            Autor autor = await _repository.GetById(id);

            if (autor == null)
            {
                TempData["message"] = Message.CreateMessage("Erro. Informe um Id válido", MessageType.Error);
                return View("Index");
            }

            AutorDTO AutorDTO = _mapper.Map<AutorDTO>(autor);

            return View("Delete", AutorDTO);
        }

        /// <summary>
        /// Delete a Autor.
        /// </summary>
        /// <param name="dto">An object of AutorDTO type.</param>
        /// <returns>View Index.</returns>
        [HttpPost]
        public async Task<IActionResult> Delete(AutorDTO dto)
        {
            if (dto.Id <= 0)
            {
                TempData["message"] = Message.CreateMessage("Erro. Informe um Id válido", MessageType.Error);
                return View(dto);
            }

            Autor autor = _mapper.Map<Autor>(dto);

            await _repository.Delete(autor);

            TempData["message"] = Message.CreateMessage("Dados excluídos com sucesso!", MessageType.Success);

            return RedirectToAction("Index");
        }
    }
}
