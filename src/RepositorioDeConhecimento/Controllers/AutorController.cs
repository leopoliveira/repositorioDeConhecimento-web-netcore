﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepositorioDeConhecimento.Infrastructure.Helpers.Messages;
using RepositorioDeConhecimento.Models.Application.DTO;
using RepositorioDeConhecimento.Models.Domain.Entities;
using RepositorioDeConhecimento.Models.Domain.Repositories;

namespace RepositorioDeConhecimento.Controllers
{
    [Authorize]
    public class AutorController : BaseController
    {
        private readonly IAutorRepository _repository;
        private readonly IMapper _mapper;

        public AutorController(IAutorRepository repository, IMapper mapper, UserManager<IdentityUser<int>> userManager) : base (userManager)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all saved Autores.
        /// </summary>
        /// <returns>Autores</returns>
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int offset = 10, int numberOfRecords = 10, string searchTerm = "")
        {
            int usuarioId = base.GetUserId();

            IEnumerable<Autor> autores;

            if (!string.IsNullOrEmpty(searchTerm) || !string.IsNullOrWhiteSpace(searchTerm))
            {
                autores = await _repository.GetWhere(a => a.IdUsuario == usuarioId &&
                                                    (a.Nome.Contains(searchTerm) ||
                                                    a.Email.Contains(searchTerm)));

                ViewBag.TotalOfPages = 0;
                ViewBag.CurrentPage = 0;

                return View(ConvertAutorToDto(autores));
            }

            autores = await _repository.GetByPages(usuarioId, page, offset, numberOfRecords);

            int totalOfRecords = await _repository.CountRecords(usuarioId);

            ViewBag.TotalOfPages = Math.Ceiling(Convert.ToDecimal(totalOfRecords) / offset);
            ViewBag.CurrentPage = page;

            return View(ConvertAutorToDto(autores));
        }

        /// <summary>
        /// Gets a specific Autor by Id.
        /// </summary>
        /// <returns>That specific Autor</returns>
        [HttpGet]
        public async Task<IActionResult> GetAutor(int id)
        {
            int usuarioId = base.GetUserId();

            Autor autor = await _repository.GetById(id, usuarioId);

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
                bool existeSigla = await _repository.AnySiglaExists(autor.Sigla, base.GetUserId());

                if (existeSigla)
                {
                    TempData["message"] = Message.CreateMessage("Erro. Já existe um Autor com essa sigla", MessageType.Error);
                    return View(dto);
                }

                autor.IdUsuario = base.GetUserId();

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
            int usuarioId = base.GetUserId();

            if (id <= 0)
            {
                TempData["message"] = Message.CreateMessage("Erro. Informe um Id válido", MessageType.Error);
                return View("Index");
            }

            Autor autor = await _repository.GetById(id, usuarioId);

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

        /// <summary>
        /// Clona um Autor.
        /// </summary>
        /// <param name="id">Id do Autor a ser clonado.</param>
        /// <returns>A View com o registro a ser salvo.</returns>
        [HttpGet]
        public async Task<IActionResult> Clonar(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("GetAutor", new { id = 0 });
            }

            int usuarioId = base.GetUserId();

            Autor autorAtual = await _repository.GetById(id, usuarioId);

            if (autorAtual == null)
            {
                return RedirectToAction("GetAutor", new { id = 0 });
            }

            Autor autorClone = _mapper.Map<Autor>(autorAtual);

            autorClone.Id = 0;

            AutorDTO dto = _mapper.Map<AutorDTO>(autorClone) ?? new AutorDTO();

            return View(dto);
        }

        private ICollection<AutorDTO> ConvertAutorToDto(IEnumerable<Autor> autores)
        {
            ICollection<AutorDTO> dtoAutores = new List<AutorDTO>();

            foreach (Autor autor in autores)
            {
                AutorDTO dto = _mapper.Map<AutorDTO>(autor);

                dtoAutores.Add(dto);
            }

            return dtoAutores;
        }   
    }
}
