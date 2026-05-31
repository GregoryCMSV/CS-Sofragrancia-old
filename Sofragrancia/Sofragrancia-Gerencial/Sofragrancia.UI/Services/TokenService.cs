using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection; 

namespace Sofragrancia.UI.Services;

public class TokenService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IServiceProvider _serviceProvider;
    private const string TOKEN_KEY = "authToken";

    public TokenService(ILocalStorageService localStorage, IServiceProvider serviceProvider)
    {
        _localStorage = localStorage;
        _serviceProvider = serviceProvider;
    }

    public async Task SalvarTokenAsync(string token)
    {
        await _localStorage.SetItemAsync(TOKEN_KEY, token);
    }

    public async Task<string?> ObterTokenAsync()
    {
        return await _localStorage.GetItemAsync<string>(TOKEN_KEY);
    }

    public async Task RemoverTokenAsync()
    {
        await _localStorage.RemoveItemAsync(TOKEN_KEY);
    }

    public async Task<bool> TokenValidoAsync()
    {
        var token = await ObterTokenAsync();
        if (string.IsNullOrWhiteSpace(token)) return false;

            return true;
        /*
        try
        {
            var httpService = _serviceProvider.GetRequiredService<HttpService>();
            
            var response = await httpService.GetAsync("api/auth/validate");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
        */
    }
}