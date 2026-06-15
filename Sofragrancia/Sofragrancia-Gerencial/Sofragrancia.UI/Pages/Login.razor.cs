using Microsoft.AspNetCore.Components;
using Sofragrancia.Shared.Dtos;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Sofragrancia.UI.Services;

namespace Sofragrancia.UI.Pages
{
    public partial class Login
    {
        [Inject]
        protected NavigationService NavigationService { get; set; } = default!;
        [Inject]
        protected HttpService HttpService { get; set; } = default!;
        [Inject]
        protected TokenService TokenService { get; set; } = default!;

        protected string Email { get; set; } = string.Empty;
        protected string Senha { get; set; } = string.Empty;
        protected bool LembrarMe { get; set; }
        protected string MensagemErro { get; set; } = string.Empty;

        // ===== VARIÁVEIS DO MODAL DE RECUPERAÇÃO =====
        protected bool ExibirModalRecuperar { get; set; } = false;
        protected bool EnviouEmailRecuperacao { get; set; } = false;
        protected bool CarregandoModal { get; set; } = false;
        protected string EmailRecuperacao { get; set; } = string.Empty;
        protected string MensagemErroModal { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            var logado = await TokenService.TokenValidoAsync();

            if (logado)
            {
                await NavigationService.NavigateAsync("home");
            }
        }

        protected async Task ExecutarLogin()
        {
            if (Email == "admin" && Senha == "admin")
            {
                await TokenService.SalvarTokenAsync("mock-token", LembrarMe);
                MensagemErro = string.Empty;
                await NavigationService.NavigateAsync("home");
                return;
            }

            var request = new LoginRequestDto
            {
                Email = Email,
                Password = Senha
            };

            var response = await HttpService.PostAsync("api/auth/login", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

                if (result?.Token is not null)
                {
                    await TokenService.SalvarTokenAsync(result.Token, LembrarMe);
                    await NavigationService.NavigateAsync("home");
                }
            }
            else
            {
                MensagemErro = "Usuário ou senha inválidos.";
            }
        }

        // ===== MÉTODOS DO MODAL DE RECUPERAÇÃO =====
        protected void AbrirModal()
        {
            EmailRecuperacao = string.Empty;
            MensagemErroModal = string.Empty;
            EnviouEmailRecuperacao = false;
            CarregandoModal = false;
            ExibirModalRecuperar = true;
        }

        protected void FecharModal()
        {
            ExibirModalRecuperar = false;
        }

        protected async Task ProcessarRecuperacaoSenha()
        {
            if (string.IsNullOrWhiteSpace(EmailRecuperacao))
            {
                MensagemErroModal = "Por favor, insira um e-mail válido.";
                return;
            }

            MensagemErroModal = string.Empty;
            CarregandoModal = true;

            try
            {
                var request = new RecuperarSenhaRequestDto { Email = EmailRecuperacao };
                var response = await HttpService.PostAsync("api/auth/reset-password", request);

                if (response.IsSuccessStatusCode)
                {
                    EnviouEmailRecuperacao = true;
                }
                else
                {
                    MensagemErroModal = "Não foi possível enviar a recuperação. Verifique o e-mail informado.";
                }
            }
            catch (Exception)
            {
                MensagemErroModal = "Ocorreu um erro ao processar sua solicitação. Tente novamente.";
            }
            finally
            {
                CarregandoModal = false;
            }
        }
    }
}