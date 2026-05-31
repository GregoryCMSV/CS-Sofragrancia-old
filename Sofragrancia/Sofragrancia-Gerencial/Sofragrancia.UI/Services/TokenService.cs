using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection; 

namespace Sofragrancia.UI.Services;

public class TokenService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IServiceProvider _serviceProvider;
    private const string TOKEN_KEY = "authToken";
    private const string LEMBRAR_KEY = "lembrarMe";
    
    // 🚀 Guarda em memória se a aba atual está em uso
    private bool _sessaoAtiva = false; 

    public TokenService(ILocalStorageService localStorage, IServiceProvider serviceProvider)
    {
        _localStorage = localStorage;
        _serviceProvider = serviceProvider;
    }

    public async Task SalvarTokenAsync(string token, bool lembrarMe)
    {
        await _localStorage.SetItemAsync(TOKEN_KEY, token);
        await _localStorage.SetItemAsync(LEMBRAR_KEY, lembrarMe);
        _sessaoAtiva = true; // 👈 Ativa a sessão na memória
    }

    public async Task<string?> ObterTokenAsync()
    {
        return await _localStorage.GetItemAsync<string>(TOKEN_KEY);
    }

    public async Task RemoverTokenAsync()
    {
        await _localStorage.RemoveItemAsync(TOKEN_KEY);
        await _localStorage.RemoveItemAsync(LEMBRAR_KEY);
        _sessaoAtiva = false; // 👈 Derruba a sessão em memória
    }

    public async Task<bool> TokenValidoAsync()
    {
        var token = await ObterTokenAsync();
        if (string.IsNullOrWhiteSpace(token)) return false;

        var lembrarMe = await _localStorage.GetItemAsync<bool>(LEMBRAR_KEY);

        // 🧠 SE A SESSÃO JÁ ESTÁ ATIVA: Significa que ele está navegando de uma tela para outra.
        // Não importa se marcou LembrarMe ou não, ele está online!
        if (_sessaoAtiva)
        {
            return true;
        }

        // 🧠 SE A SESSÃO NÃO ESTÁ ATIVA: Significa que o app acabou de ligar (F5 ou nova aba).
        if (!lembrarMe)
        {
            // Se ele não pediu para persistir, limpamos tudo e mandamos pro login.
            await RemoverTokenAsync();
            return false;
        }

        // Se ele pediu para lembrar, reativamos a sessão em memória e deixamos passar
        _sessaoAtiva = true;
        return true;
    }
}