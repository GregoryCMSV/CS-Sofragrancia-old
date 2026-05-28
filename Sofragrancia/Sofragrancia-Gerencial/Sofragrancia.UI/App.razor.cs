using Microsoft.AspNetCore.Components;
using Sofragrancia.UI.Services;

namespace Sofragrancia.UI;

public class AppBase : ComponentBase
{
    [Inject]
    protected TokenService TokenService { get; set; } = default!;

    [Inject]
    protected NavigationManager Navigation { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        var rotaAtual =
            Navigation.ToBaseRelativePath(Navigation.Uri);

        if (string.IsNullOrEmpty(rotaAtual))
            rotaAtual = "login";

        if (rotaAtual.ToLower() == "login")
            return;

        var tokenValido =
            await TokenService.TokenValidoAsync();

        if (!tokenValido)
        {
            Navigation.NavigateTo("/login", true);
        }
    }
}