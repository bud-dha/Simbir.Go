FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Simbir.Go/Simbir.Go.csproj", "Simbir.Go/"]
COPY ["Simbir.Go.BLL/Simbir.Go.BLL.csproj", "Simbir.Go.BLL/"]
COPY ["Simbir.Go.DAL/Simbir.Go.DAL.csproj", "Simbir.Go.DAL/"]
RUN dotnet restore "Simbir.Go/Simbir.Go.csproj"
COPY . .
WORKDIR "/src/Simbir.Go"
RUN dotnet build "Simbir.Go.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Simbir.Go.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Simbir.Go.dll"]