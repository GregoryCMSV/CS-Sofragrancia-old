using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Sofragrancia.UI.Components;

namespace Sofragrancia.UI.Pages;

public partial class Settings : ComponentBase
{
    // Controla de forma fluida qual aba de configuração está ativa na tela
    protected string abaAtiva = "perfil";

    // REMOVIDOS os objetos "Perfil" e "NovoUsuario" antigos que causavam o erro de compilação,
    // já que agora MyProfile e NewUser gerenciam seus próprios DTOs de forma isolada.

    protected List<TabNavigation.TabItem> tabs = new()
    {
        new()
        {
            Id = "perfil",
            Title = "Meu Perfil",
            Icon = "👤"
        },
        new()
        {
            Id = "cadastrar",
            Title = "Cadastrar Colaborador",
            Icon = "➕"
        }
    };

    protected void AlternarAba(string novaAba)
    {
        abaAtiva = novaAba;
    }
}