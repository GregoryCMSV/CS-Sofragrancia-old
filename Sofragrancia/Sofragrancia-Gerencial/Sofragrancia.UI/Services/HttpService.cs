using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Sofragrancia.UI.Services;

public class HttpService
{
    private readonly HttpClient _httpClient;
    private readonly TokenService _tokenService;
    public event Action? OnUnauthorized;

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
        else
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }

private async Task<HttpResponseMessage> SendAsync(
    string endpoint,
    Func<Task<HttpResponseMessage>> request)
{
    await ConfigurarTokenAsync();

    var response = await request();

    if (response.StatusCode == HttpStatusCode.Unauthorized &&
        !endpoint.Contains("api/auth/login"))
    {
        await _tokenService.RemoverTokenAsync();

        _httpClient.DefaultRequestHeaders.Authorization = null;

        OnUnauthorized?.Invoke();
    }
    return response;
}

public async Task<HttpResponseMessage> GetAsync(string endpoint)
    => await SendAsync(endpoint, () => _httpClient.GetAsync(endpoint));

public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
    => await SendAsync(endpoint, () => _httpClient.PostAsJsonAsync(endpoint, data));

public async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T data)
    => await SendAsync(endpoint, () => _httpClient.PutAsJsonAsync(endpoint, data));

public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
    => await SendAsync(endpoint, () => _httpClient.DeleteAsync(endpoint));

}