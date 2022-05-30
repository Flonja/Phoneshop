#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Phoneshop.BlazorApp/Phoneshop.BlazorApp.csproj", "Phoneshop.BlazorApp/"]
COPY ["Phoneshop.Infrastructure/Phoneshop.Infrastructure.csproj", "Phoneshop.Infrastructure/"]
COPY ["Phoneshop.Domain/Phoneshop.Domain.csproj", "Phoneshop.Domain/"]
RUN dotnet restore "Phoneshop.BlazorApp/Phoneshop.BlazorApp.csproj"
COPY . .
WORKDIR "/src/Phoneshop.BlazorApp"
RUN dotnet build "Phoneshop.BlazorApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Phoneshop.BlazorApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Phoneshop.BlazorApp.dll"]