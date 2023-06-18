FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/api/TechBox.Api/TechBox.Api.csproj", "src/api/TechBox.Api/"]
RUN dotnet restore "src/api/TechBox.Api/TechBox.Api.csproj"
COPY . .
WORKDIR "/src/src/api/TechBox.Api"
RUN dotnet build "TechBox.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TechBox.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechBox.Api.dll"]