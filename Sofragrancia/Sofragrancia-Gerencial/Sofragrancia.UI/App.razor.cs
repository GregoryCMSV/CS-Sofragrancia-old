using Microsoft.AspNetCore.Components;
using Sofragrancia.UI.Services;

namespace Sofragrancia.UI;

public class AppBase : ComponentBase
{
    [Inject] protected TokenService TokenService { get; set; } = default!;

    [Inject] protected NavigationManager Navigation { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        try
        {
            var rotaAtual = Navigation.ToBaseRelativePath(Navigation.Uri);
            if (string.IsNullOrEmpty(rotaAtual)) rotaAtual = "login";

            bool isTokenValido = await TokenService.TokenValidoAsync();

            if (rotaAtual.ToLower() == "login")
            {
                if (isTokenValido)
                {
                    Navigation.NavigateTo("/home", forceLoad: false);
                }
                return;
            }

            if (!isTokenValido)
            {
                Navigation.NavigateTo("/login", forceLoad: false);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro na validação global de rota: {ex.Message}");
            Navigation.NavigateTo("/login", forceLoad: false);
        }
    }
}