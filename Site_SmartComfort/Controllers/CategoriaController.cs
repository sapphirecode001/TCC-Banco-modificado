using Microsoft.AspNetCore.Mvc;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;

namespace Site_SmartComfort.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ILogger<CategoriaController> _logger;
        private ICategoriaRepository _categoriaRepository;
        public CategoriaController(ILogger<CategoriaController> logger, ICategoriaRepository categoriaRepository)
        {
            _logger = logger;
            _categoriaRepository = categoriaRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CadCategoria()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadCategoria(Categoria categoria) 
        {
            _categoriaRepository.CadastrarCategoria(categoria);
            return View();
        }
    }
}
