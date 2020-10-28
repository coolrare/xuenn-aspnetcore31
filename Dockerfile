FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["api1.csproj", "./"]
RUN dotnet restore "api1.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "api1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "api1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api1.dll"]
