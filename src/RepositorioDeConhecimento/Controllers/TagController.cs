using Microsoft.AspNetCore.Mvc;

namespace RepositorioDeConhecimento.Controllers
{
    public class TagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
