using Domain;
using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ProdutoService _service;

        public ProdutosController(ProdutoService service) => _service = service;

        private int? UsuarioId() => HttpContext.Session.GetInt32("UsuarioId");

        // Listagem
        public async Task<IActionResult> Index(int? editId = null)
        {
            if (UsuarioId() is null) return RedirectToAction("Index", "Login");

            var produtos = await _service.GetProdutosAsync();
            ViewBag.EditId = editId; // usado para edição inline
            return View(produtos);
        }

        // Cadastro
        [HttpPost]
        public async Task<IActionResult> Create(Produto produto)
        {
            if (UsuarioId() is null) return RedirectToAction("Index", "Login");

            try
            {
                await _service.AddProdutoAsync(produto, UsuarioId()!.Value);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao cadastrar produto: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        // GET Edit (abre tela ou ativa edição inline)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (UsuarioId() is null) return RedirectToAction("Index", "Login");

            var produtos = await _service.GetProdutosAsync();
            var produto = produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null) return NotFound();

            return View(produto); // se quiser tela separada Edit.cshtml
        }

        // POST Edit (salva alterações)
        [HttpPost]
        public async Task<IActionResult> Edit(Produto produto)
        {
            if (UsuarioId() is null) return RedirectToAction("Index", "Login");

            try
            {
                await _service.UpdateProdutoAsync(produto, UsuarioId()!.Value);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao atualizar produto: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        // Exclusão
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (UsuarioId() is null) return RedirectToAction("Index", "Login");

            try
            {
                await _service.DeleteProdutoAsync(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao excluir produto: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
