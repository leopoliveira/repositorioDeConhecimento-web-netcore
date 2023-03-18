using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RepositorioDeConhecimento.Controllers
{
    [ValidateAntiForgeryToken] // Valida se o Token está na requisição
    [AutoValidateAntiforgeryToken] // Valida o Token
    public class BaseController : Controller
    {
        private readonly UserManager<IdentityUser<int>> _userManager;

        public BaseController(UserManager<IdentityUser<int>> userManager)
        {
            _userManager = userManager;
        }

        public int GetUserId()
        {
            if (int.TryParse(_userManager.GetUserId(HttpContext.User), out int usuarioId))
            {
                return usuarioId;
            }

            return 0;
        }
    }
}
