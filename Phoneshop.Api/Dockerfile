#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Phoneshop.Api/Phoneshop.Api.csproj", "Phoneshop.Api/"]
COPY ["Phoneshop.Infrastructure/Phoneshop.Infrastructure.csproj", "Phoneshop.Infrastructure/"]
COPY ["Phoneshop.Domain/Phoneshop.Domain.csproj", "Phoneshop.Domain/"]
RUN dotnet restore "Phoneshop.Api/Phoneshop.Api.csproj"
COPY . .
WORKDIR "/src/Phoneshop.Api"
RUN dotnet build "Phoneshop.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Phoneshop.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Phoneshop.Api.dll"]