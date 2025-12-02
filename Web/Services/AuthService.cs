using System.Net.Http;
using System.Net.Http.Json;
using Domain;

namespace Web.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly string _base;

        public AuthService(IHttpClientFactory factory, IConfiguration config)
        {
            _http = factory.CreateClient();
            _base = config["ApiBaseUrl"] ?? "https://localhost:7206/";
            _http.BaseAddress = new Uri(_base);
        }

        public async Task<Usuario?> LoginAsync(string nome, string senha)
        {
            try
            {
                var usuarios = await _http.GetFromJsonAsync<List<Usuario>>("Usuarios");

                if (usuarios == null)
                    return null;

                return usuarios.FirstOrDefault(u => u.Nome == nome && u.Senha == senha && u.Status);
            }
            catch (Exception ex)
            {
                // Aqui você pode logar o erro ou apenas retornar null
                Console.WriteLine($"Erro ao acessar API: {ex.Message}");
                return null;
            }
        }

    }
}
