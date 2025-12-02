using System.Net.Http;
using System.Net.Http.Json;
using Domain;

namespace Web.Services
{
    public class ProdutoService
    {
        private readonly HttpClient _http;

        public ProdutoService(IHttpClientFactory factory, IConfiguration config)
        {
            _http = factory.CreateClient();
            var baseUrl = config["ApiBaseUrl"] ?? "https://localhost:7206/";
            _http.BaseAddress = new Uri(baseUrl);
        }

        // Buscar todos os produtos
        public async Task<List<Produto>> GetProdutosAsync()
        {
            var resp = await _http.GetAsync("api/Produtos");

            if (!resp.IsSuccessStatusCode)
            {
                var error = await resp.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao buscar produtos: {resp.StatusCode} - {error}");
            }

            return await resp.Content.ReadFromJsonAsync<List<Produto>>() ?? new List<Produto>();
        }

        // Cadastrar novo produto (com usuário logado)
        public async Task<Produto?> AddProdutoAsync(Produto produto, int usuarioId)
        {
            produto.IdUsuarioCadastro = usuarioId;
            produto.IdUsuarioUpdate = usuarioId; // no cadastro inicial

            var resp = await _http.PostAsJsonAsync("api/Produtos", produto);

            if (!resp.IsSuccessStatusCode)
            {
                var error = await resp.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao cadastrar produto: {resp.StatusCode} - {error}");
            }

            return await resp.Content.ReadFromJsonAsync<Produto>();
        }

        // Atualizar produto existente
        public async Task UpdateProdutoAsync(Produto produto, int usuarioId)
        {
            produto.IdUsuarioUpdate = usuarioId;

            var resp = await _http.PutAsJsonAsync($"api/Produtos/{produto.Id}", produto);

            if (!resp.IsSuccessStatusCode)
            {
                var error = await resp.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao atualizar produto: {resp.StatusCode} - {error}");
            }
        }

        // Excluir produto
        public async Task DeleteProdutoAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/Produtos/{id}");

            if (!resp.IsSuccessStatusCode)
            {
                var error = await resp.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao excluir produto: {resp.StatusCode} - {error}");
            }
        }
    }
}
