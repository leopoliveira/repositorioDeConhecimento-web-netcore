using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RepositorioDeConhecimento.Infrastructure.Context;
using RepositorioDeConhecimento.Infrastructure.Helpers.Messages;
using RepositorioDeConhecimento.Models.Domain.Entities;
using RepositorioDeConhecimento.Models.Domain.Repositories;
using RepositorioDeConhecimento.Models.ViewModels;

namespace RepositorioDeConhecimento.Controllers
{
    [AllowAnonymous]
    [ValidateAntiForgeryToken] // Valida se o Token está na requisição
    [AutoValidateAntiforgeryToken] // Valida o Token
    public class UsuarioController : BaseController
    {
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly IAutorRepository _autorRepository;

        public UsuarioController(SignInManager<IdentityUser<int>> signInManager, UserManager<IdentityUser<int>> userManager, IAutorRepository autorRepository) : base(userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _autorRepository = autorRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User?.Identity == null || !User.Identity.IsAuthenticated)
            {
                return View();
            }

            return RedirectToAction("Index", "Conhecimento");
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool usuarioExiste = await _userManager.FindByNameAsync(model.Usuario) != null;
                bool emailExiste = await _userManager.FindByEmailAsync(model.Email) != null;
                IEnumerable<Autor> autorComASigla = await _autorRepository.GetWhere(a => a.Sigla == model.Sigla);

                if (usuarioExiste)
                {
                    TempData["message"] = Message.CreateMessage("Erro. Usuário já existente.", MessageType.Error);
                    return View(model);
                }

                if (emailExiste)
                {
                    TempData["message"] = Message.CreateMessage("Erro. E-mail já existente.", MessageType.Error);
                    return View(model);
                }

                if (autorComASigla?.Count() > 0)
                {
                    TempData["message"] = Message.CreateMessage("Erro. Sigla já existente.", MessageType.Error);
                    return View(model);
                }

                var usuario = new IdentityUser<int>
                {
                    UserName = model.Usuario,
                    Email = model.Email
                };

                var resultadoCadastro = await _userManager.CreateAsync(usuario, model.Senha);

                if (resultadoCadastro.Succeeded)
                {
                    await CadastraAutor(model.NomeCompleto, model.Sigla, model.Email, usuario.Id);

                    await _signInManager.SignInAsync(usuario, isPersistent: false);

                    return RedirectToAction("Index", "Conhecimento");
                }

                foreach (var error in resultadoCadastro.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUsuarioViewModel model, string? redirectUrl)
        {
            if (ModelState.IsValid)
            {
                var resultadoLogin = await _signInManager
                                        .PasswordSignInAsync(model.Usuario,
                                                            model.Senha,
                                                            isPersistent: model.LembrarMe,
                                                            lockoutOnFailure: false);

                if (resultadoLogin.Succeeded)
                {
                    if (Url.IsLocalUrl(redirectUrl))
                    {
                        return LocalRedirect(redirectUrl);
                    }

                    return RedirectToAction("Index", "Conhecimento");
                }

                TempData["message"] = Message.CreateMessage("Erro ao fazer Login.", MessageType.Error);
            }

            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            TempData["message"] = Message.CreateMessage("Até a próxima!", MessageType.Success);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult RecuperaSenha(RegistrarUsuarioViewModel model)
        {
            return Ok();
        }

        /*[HttpGet]
        public async Task<IActionResult> EditarUsuario(RegistrarUsuarioViewModel model)
        {
        
        }*/

        private async Task CadastraAutor(string nomeCompleto, string sigla, string email, int usuarioId)
        {
            Autor autor = new Autor
            {
                Nome = nomeCompleto,
                Sigla = sigla,
                Email = email,
                IdUsuario = usuarioId
            };

            await _autorRepository.Insert(autor);
        }
    }
}
