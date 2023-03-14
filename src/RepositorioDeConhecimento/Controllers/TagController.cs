using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositorioDeConhecimento.Infrastructure.Helpers.Messages;
using RepositorioDeConhecimento.Models.Application.DTO;
using RepositorioDeConhecimento.Models.Domain.Entities;
using RepositorioDeConhecimento.Models.Domain.Repositories;

namespace RepositorioDeConhecimento.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly ITagRepository _repository;
        private readonly IMapper _mapper;

        public TagController(ITagRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all saved Tags.
        /// </summary>
        /// <returns>Tags</returns>
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int offset = 10, int numberOfRecords = 10)
        {
            IEnumerable<Tag> tags = await _repository.GetByPages(page, offset, numberOfRecords);

            ICollection<TagDTO> dtoTags = new List<TagDTO>();

            foreach (Tag tag in tags)
            {
                TagDTO dto = _mapper.Map<TagDTO>(tag);

                dtoTags.Add(dto);
            }

            return View(dtoTags);
        }

        /// <summary>
        /// Gets a specific Tag by Id.
        /// </summary>
        /// <returns>That specific Tag</returns>
        [HttpGet]
        public async Task<IActionResult> GetTag(int id)
        {
            Tag tag = await _repository.GetById(id);

            TagDTO dto = _mapper.Map<TagDTO>(tag) ?? new TagDTO();

            return View("Save", dto);
        }

        /// <summary>
        /// Save a Tag.
        /// </summary>
        /// <param name="dto">An object of TagDTO type.</param>
        /// <returns>View Index.</returns>
        [HttpPost]
        public async Task<IActionResult> Save(TagDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["message"] = Message.CreateMessage("Dados inválidos. Por favor, revise os dados inseridos", MessageType.Error);

                return View(dto);
            }

            Tag tag = _mapper.Map<Tag>(dto);

            if (tag.Id > 0)
            {
                await _repository.Update(tag);
            }
            else
            {
                await _repository.Insert(tag);
            }


            TempData["message"] = Message.CreateMessage("Dados salvos com sucesso!", MessageType.Success);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Delete a Tag.
        /// </summary>
        /// <param name="id">Tag Id.</param>
        /// <returns>View Delete.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData["message"] = Message.CreateMessage("Erro. Informe um Id válido", MessageType.Error);
                return View("Index");
            }

            Tag tag = await _repository.GetById(id);

            if (tag == null)
            {
                TempData["message"] = Message.CreateMessage("Erro. Informe um Id válido", MessageType.Error);
                return View("Index");
            }

            TagDTO TagDTO = _mapper.Map<TagDTO>(tag);

            return View("Delete", TagDTO);
        }

        /// <summary>
        /// Delete a Tag.
        /// </summary>
        /// <param name="dto">An object of TagDTO type.</param>
        /// <returns>View Index.</returns>
        [HttpPost]
        public async Task<IActionResult> Delete(TagDTO dto)
        {
            if (dto.Id <= 0)
            {
                TempData["message"] = Message.CreateMessage("Erro. Informe um Id válido", MessageType.Error);
                return View(dto);
            }

            Tag tag = _mapper.Map<Tag>(dto);

            await _repository.Delete(tag);

            TempData["message"] = Message.CreateMessage("Dados excluídos com sucesso!", MessageType.Success);

            return RedirectToAction("Index");
        }
    }
}
