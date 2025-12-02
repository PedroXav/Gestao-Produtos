using Domain;
using System.Net.Http;
using System.Net.Http.Json;


namespace Desktop.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _http;

        public UsuarioService()
        {
            var handler = new HttpClientHandler
            {
                // Ignora validação de certificado SSL (apenas para desenvolvimento)
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _http = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7206/")
            };
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            try
            {
                var usuarios = await _http.GetFromJsonAsync<List<Usuario>>("Usuarios");
                return usuarios ?? new List<Usuario>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao acessar API: {ex.Message}");
                return new List<Usuario>();
            }
        }

        public async Task<bool> AddUsuarioAsync(Usuario usuario)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("Usuarios", usuario);
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Status: {response.StatusCode}");
                Console.WriteLine($"Resposta: {result}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar usuário: {ex.Message}");
                return false;
            }
        }

        public async Task UpdateUsuarioAsync(Usuario usuario)
        {
            await _http.PutAsJsonAsync($"Usuarios/{usuario.Id}", usuario);
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            await _http.DeleteAsync($"Usuarios/{id}");
        }
    }
}
