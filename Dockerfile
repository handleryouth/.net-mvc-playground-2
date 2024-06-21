FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["contactForm.csproj", "./"]
RUN dotnet restore "contactForm.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "contactForm.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "contactForm.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8000:80
ENTRYPOINT ["dotnet", "contactForm.dll"]
