using Microsoft.AspNetCore.Components;

namespace Sofragrancia.UI.Pages // Garanta que o nome seja EXATAMENTE este
{
    public partial class Alerts : ComponentBase // Adicione o ": ComponentBase" para ajudar o compilador
    {
        private string abaAtiva = "configurar";

        private void AlternarAba(string novaAba)
        {
            abaAtiva = novaAba;
            StateHasChanged();
        }
    }
}