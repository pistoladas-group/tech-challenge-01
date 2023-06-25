FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/web/TechBox.Web/TechBox.Web.csproj", "src/web/TechBox.Web/"]
RUN dotnet restore "src/web/TechBox.Web/TechBox.Web.csproj"
COPY . .
WORKDIR "/src/src/web/TechBox.Web"
RUN dotnet build "TechBox.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TechBox.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechBox.Web.dll"]