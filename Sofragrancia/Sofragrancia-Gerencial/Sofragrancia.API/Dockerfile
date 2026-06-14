# Esta fase é usada durante a execução no VS no modo rápido
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia os arquivos de projeto (API e suas dependências)
COPY ["Sofragrancia.API/Sofragrancia.API.csproj", "Sofragrancia.API/"]
COPY ["Sofragrancia.Banco/Sofragrancia.Banco.csproj", "Sofragrancia.Banco/"]
COPY ["Sofragrancia.Shared/Sofragrancia.Shared.csproj", "Sofragrancia.Shared/"]
COPY ["Sofragrancia.EmailSender/Sofragrancia.EmailSender.csproj", "Sofragrancia.EmailSender/"]

# Restaura as dependências do projeto principal
RUN dotnet restore "./Sofragrancia.API/Sofragrancia.API.csproj"

# Copia o restante de todo o código da solução
COPY . .

# Faz o build
WORKDIR "/src/Sofragrancia.API"
RUN dotnet build "./Sofragrancia.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sofragrancia.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase final é usada na produção
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sofragrancia.API.dll"]