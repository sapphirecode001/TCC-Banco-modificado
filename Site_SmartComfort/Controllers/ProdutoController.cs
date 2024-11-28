using Microsoft.AspNetCore.Mvc;

namespace Site_SmartComfort.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Produto()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View(_);
        }
    }
}
