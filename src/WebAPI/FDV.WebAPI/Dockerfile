FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./WebAPI/FDV.WebAPI/FDV.WebAPI.csproj", "WebAPI/FDV.WebAPI/"]
COPY ["./Autenticacao/FDV.Identidade.App/FDV.Identidade.App.csproj", "Autenticacao/FDV.Identidade.App/"]
COPY ["./Usuarios/FDV.Usuarios.App/FDV.Usuarios.App.csproj", "Usuarios/FDV.Usuarios.App/"]
COPY ["./BuildingBlocks/FDV.Core/FDV.Core.csproj", "BuildingBlocks/FDV.Core/"]
COPY ["./Forum/FDV.Forum.Infra/FDV.Forum.Infra.csproj", "Forum/FDV.Forum.Infra/"]
COPY ["./Forum/FDV.Forum.Domain/FDV.Forum.Domain.csproj", "Forum/FDV.Forum.Domain/"]
COPY ["./BuildingBlocks/FDV.WebApi.Core/FDV.WebApi.Core.csproj", "BuildingBlocks/FDV.WebApi.Core/"]
COPY ["./Forum/FDV.Forum.App/FDV.Forum.App.csproj", "Forum/FDV.Forum.App/"]
RUN dotnet restore "WebAPI/FDV.WebAPI/FDV.WebAPI.csproj"
COPY . .
WORKDIR "WebAPI/FDV.WebAPI"
RUN dotnet build "FDV.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FDV.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FDV.WebAPI.dll"]
