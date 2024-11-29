using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;

namespace Site_SmartComfort.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ILogger<ProdutoController> _logger;
        private IProdutoRepository _produtoRepository;
        private ICategoriaRepository _categoriaRepository;

        public ProdutoController(ILogger<ProdutoController> logger, IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _logger = logger;
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;

        }

        public IActionResult Index()
        {
            return View(_produtoRepository.ObterTodosProdutos());
        }

        public IActionResult Produto()
        {
        return View();
        }

        [HttpGet]
        public IActionResult CadProduto()
        {
            var listaCategoria = _categoriaRepository.ObterTodosCategorias();
            ViewBag.ListaCategorias = new SelectList(listaCategoria, "id", "NomeCategoria");
                return View();
        }

        [HttpPost]
        public IActionResult CadProduto(Produto produto)
        {
            var listaCategoria = _categoriaRepository.ObterTodosCategorias();
            ViewBag.ListaCategorias = new SelectList(listaCategoria, "id", "NomeCategoria");
            _produtoRepository.CadastrarProduto(produto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult editarProduto(int id)
        { 
            return View(_produtoRepository.ObterProduto(id));
        }

        [HttpPost]
        public IActionResult editarProduto(Produto produto)
        {
            _produtoRepository.AtualizarProduto(produto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _produtoRepository.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
