using Microsoft.AspNetCore.Mvc;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;
using System.Diagnostics;

namespace Site_SmartComfort.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProdutoRepository _produtoRepository;

        // Combine os dois construtores em um único
        public HomeController(ILogger<HomeController> logger, IProdutoRepository produtoRepository)
        {
            _logger = logger;
            _produtoRepository = produtoRepository;
        }

        public IActionResult Index()
        {
            var produtos = _produtoRepository.ObterTodosProdutos();  // Obtém todos os produtos cadastrados
            return View(produtos); // Passa os produtos para a view
        }
        public IActionResult SobreNos()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
