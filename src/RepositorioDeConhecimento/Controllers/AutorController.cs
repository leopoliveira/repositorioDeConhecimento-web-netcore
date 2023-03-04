using Microsoft.AspNetCore.Mvc;

namespace RepositorioDeConhecimento.Controllers
{
    public class AutorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
