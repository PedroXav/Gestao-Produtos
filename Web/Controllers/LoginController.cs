using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly AuthService _auth;

        public LoginController(AuthService auth) => _auth = auth;

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(string nome, string senha)
        {
            var usuario = await _auth.LoginAsync(nome, senha);
            if (usuario == null)
            {
                ViewBag.Error = "Usuário inválido ou inativo.";
                return View();
            }

            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            HttpContext.Session.SetString("UsuarioNome", usuario.Nome);

            return RedirectToAction("Index", "Produtos");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
