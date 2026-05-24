using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Sofragrancia.UI.Services;

public class HttpService
{
    private readonly HttpClient _httpClient;
    private readonly TokenService _tokenService;

    public HttpService(
        HttpClient httpClient,
        TokenService tokenService)
    {
        _httpClient = httpClient;
        _tokenService = tokenService;
    }

    private async Task ConfigurarTokenAsync()
    {
        var token = await _tokenService.ObterTokenAsync();

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<HttpResponseMessage> GetAsync(string endpoint)
    {
        await ConfigurarTokenAsync();

        return await _httpClient.GetAsync(endpoint);
    }

    public async Task<HttpResponseMessage> PostAsync<T>(
        string endpoint,
        T data)
    {
        await ConfigurarTokenAsync();

        return await _httpClient.PostAsJsonAsync(endpoint, data);
    }
}