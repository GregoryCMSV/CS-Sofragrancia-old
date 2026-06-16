using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Sofragrancia.API.Services;
using Sofragrancia.Banco;
using Sofragrancia.Banco.Repositories;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Sofragrancia.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var supabaseUrl = builder.Configuration["Supabase:Url"];
            var supabaseKey = builder.Configuration["Supabase:Key"];
            var baseInstance = await SofragranciaBaseConnection.GetInstanceAsync(supabaseUrl, supabaseKey);
            builder.Services.AddScoped(_ => baseInstance.SupabaseClient);
            var jwtSecret = @"{
                ""x"": ""BTc30wJglqPCZZH-AtrkU2nFDsNKxQvzVaqqykQtxoM"",
                ""y"": ""wmWhwyEkNdbem6Y23Gpg1_bnIC6lJSsUDzg9y_9QU_0"",
                ""alg"": ""ES256"",
                ""crv"": ""P-256"",
                ""ext"": true,
                ""kid"": ""b01e51b2-e2b5-477c-a1b7-e056c01d163a"",
                ""kty"": ""EC"",
                ""key_ops"": [ ""verify"" ]
            }";

            builder.Services.AddSingleton<RabbitMqService>();
            builder.Services.AddScoped<AlertService>();

            builder.Services.AddScoped<ProdutoRepository>();
            builder.Services.AddScoped<ProdutoIntegracaoService>();
            builder.Services.AddHttpClient<FinanceiroService>(client =>
            {
                client.BaseAddress = new Uri(
                    builder.Configuration["FinanceiroApi:BaseUrl"]
                );

                client.DefaultRequestHeaders.Add(
                    "apikey",
                    builder.Configuration["FinanceiroApi:ApiKey"]
                );

                client.DefaultRequestHeaders.Add(
                    "Authorization",
                    $"Bearer {builder.Configuration["FinanceiroApi:ApiKey"]}"
                );
            });

            var ecdsaKey = new JsonWebKey(jwtSecret);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = ecdsaKey,
                        ValidAudience = "authenticated",
                        ValidateIssuer = false,
                        ValidateAudience = true
                    };
                });

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("PermitirFrontEnd", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // if (app.Environment.IsDevelopment())
            // {
            app.MapOpenApi();

            app.MapScalarApiReference("/", options =>
            {
                options.WithTitle("API Gerencial");
            });
            // }

            app.UseHttpsRedirection();

            app.UseCors("PermitirFrontEnd");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            app.Run();
        }
    }
}
