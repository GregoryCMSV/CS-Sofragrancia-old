using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Sofragrancia.UI.Services;

public class NavigationService
{
    private readonly NavigationManager _navigation;
    private readonly TokenService _tokenService;

    public NavigationService(
        NavigationManager navigation,
        TokenService tokenService,
        HttpService httpService)
    {
        _navigation = navigation;
        _tokenService = tokenService;

        // Ouve o evento do HttpService. Quando a API rejeitar o token, ele limpa tudo.
        httpService.OnUnauthorized += HandleUnauthorized;
    }

    // 🛠️ Ajustado para rodar em background com segurança sem travar a UI (Fire and Forget)
    private void HandleUnauthorized()
    {
        _ = Task.Run(async () =>
        {
            await _tokenService.RemoverTokenAsync();
            _navigation.NavigateTo("", true);
        });
    }

    public async Task NavigateAsync(string destino)
    {
        if (destino == "login" || destino.Equals(""))
        {
            await _tokenService.RemoverTokenAsync();
            _navigation.NavigateTo("", true);
            return;
        }

        if (await _tokenService.TokenValidoAsync())
        {
            _navigation.NavigateTo(destino);
            return;
        }

        await _tokenService.RemoverTokenAsync();
        _navigation.NavigateTo("", true);
    }
}