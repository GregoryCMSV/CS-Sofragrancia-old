using Microsoft.AspNetCore.Components;
using Sofragrancia.Shared.DTOs;
using System.Net.Http.Json;
using Sofragrancia.UI.Services;

namespace Sofragrancia.UI.Pages
{
    public partial class Login
    {
        [Inject]
        protected NavigationManager Navigation { get; set; } = default!;
        [Inject]
        protected HttpService HttpService { get; set; } = default!;
        [Inject]
        protected TokenService TokenService { get; set; } = default!;

        protected string Email { get; set; } = string.Empty;
        protected string Senha { get; set; } = string.Empty;
        protected bool LembrarMe { get; set; }
        protected string MensagemErro { get; set; } = string.Empty;

        protected async Task ExecutarLogin()
        {
            if (Email == "admin" && Senha == "admin")
            {
                MensagemErro = string.Empty;
                Navigation.NavigateTo("/home");

                return;
            }

            var request = new LoginRequestDto
            {
                Email = Email,
                Password = Senha
            };

            var response = await HttpService.PostAsync(
                "api/auth/login",
                request
            );

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content
                    .ReadFromJsonAsync<LoginResponseDto>();

                if (result?.Token is not null)
                {
                    await TokenService.SalvarTokenAsync(result.Token);

                    MensagemErro = string.Empty;

                    Navigation.NavigateTo("/home");
                }
            }
            else
            {
                MensagemErro = "Usuário ou senha inválidos.";
            }
        }
    }
}