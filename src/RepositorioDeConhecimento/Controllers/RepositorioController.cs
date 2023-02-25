using Microsoft.AspNetCore.Mvc;

namespace RepositorioDeConhecimento.Controllers
{
    public class RepositorioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
