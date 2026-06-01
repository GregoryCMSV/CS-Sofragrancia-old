using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection; 
using System;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Sofragrancia.Shared.Dtos;

namespace Sofragrancia.UI.Services;

public class TokenService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IServiceProvider _serviceProvider;
    private const string TOKEN_KEY = "authToken";
    private const string LEMBRAR_KEY = "lembrarMe";
    
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
        _sessaoAtiva = true; 
    }

    public async Task<string?> ObterTokenAsync()
    {
        return await _localStorage.GetItemAsync<string>(TOKEN_KEY);
    }

    public async Task RemoverTokenAsync()
    {
        await _localStorage.RemoveItemAsync(TOKEN_KEY);
        await _localStorage.RemoveItemAsync(LEMBRAR_KEY);
        _sessaoAtiva = false; 
    }

    public async Task<bool> TokenValidoAsync()
    {
        var token = await ObterTokenAsync();
        if (string.IsNullOrWhiteSpace(token)) return false;

        var lembrarMe = await _localStorage.GetItemAsync<bool>(LEMBRAR_KEY);

        if (!_sessaoAtiva && !lembrarMe)
        {
            await RemoverTokenAsync();
            return false;
        }
        
        _sessaoAtiva = true;
        return true;
        /*
        try
        {
            var httpService = _serviceProvider.GetRequiredService<HttpService>();
            var response = await httpService.GetAsync("api/auth/validate");

            if (!response.IsSuccessStatusCode)
            {
                await RemoverTokenAsync();
                return false;
            }
            var resultado = await response.Content.ReadFromJsonAsync<RespostaValidacaoTokenDto>();

            if (resultado is not null && resultado.IsValid)
            {
                _sessaoAtiva = true;
                return true;
            }
            await RemoverTokenAsync();
            return false;
        }
        catch (Exception)
        {
            await RemoverTokenAsync();
            return false;
        }
        */
    }
}