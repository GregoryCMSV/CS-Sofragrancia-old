using Blazored.LocalStorage;
namespace Sofragrancia.UI.Services;

public class TokenService
{
    private readonly ILocalStorageService _localStorage;
    private const string TOKEN_KEY = "authToken";
    public TokenService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
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
}