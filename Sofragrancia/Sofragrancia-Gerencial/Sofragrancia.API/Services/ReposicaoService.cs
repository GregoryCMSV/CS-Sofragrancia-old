using System.Net.Http.Json;
using Sofragrancia.API.DTOs;

namespace Sofragrancia.API.Services
{
    public class ReposicaoService
    {
        private readonly HttpClient _httpClient;

        public ReposicaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReposicaoDto>> ObterReposicoes()
        {
            var response = await _httpClient.GetAsync("/api/Reposicoes");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<ReposicaoDto>>();
        }
    }
}