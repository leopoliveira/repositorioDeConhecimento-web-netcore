using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositorioDeConhecimento.Models.ViewModels;

namespace RepositorioDeConhecimento.Controllers
{
    [AllowAnonymous]
    public class UsuarioController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(LoginUsuarioViewModel model)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Login(LoginUsuarioViewModel model)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult RecuperaSenha()
        {
            return Ok();
        }
    }
}
