using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Sofragrancia.UI.Components;
using Sofragrancia.UI.Services;

namespace Sofragrancia.UI.Pages;

public partial class Settings : ComponentBase
{
    [Inject] protected TokenService TokenService { get; set; } = default!;

protected string abaAtiva = "perfil";
    protected List<TabNavigation.TabItem> tabs = new();

    protected override async Task OnInitializedAsync()
    {
        // 1. Define a lista base
        tabs = new()
        {
            new() { Id = "perfil", Title = "Meu Perfil", Icon = "👤" },
            new() { Id = "cadastrar", Title = "Cadastrar Colaborador", Icon = "➕" }
        };

        // 2. Busca o usuário e bloqueia a aba se não for Admin
        var usuario = await TokenService.ObterDadosUsuarioLogadoAsync();
        
        if (usuario?.Role != "Admin")
        {
            tabs.RemoveAll(t => t.Id == "cadastrar");
        }
    }

    protected void AlternarAba(string novaAba)
    {
        abaAtiva = novaAba;
    }
}