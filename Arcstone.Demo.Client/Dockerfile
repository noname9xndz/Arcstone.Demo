#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Arcstone.Demo.Client/Arcstone.Demo.Client.csproj", "Arcstone.Demo.Client/"]
RUN dotnet restore "Arcstone.Demo.Client/Arcstone.Demo.Client.csproj"
COPY . .
WORKDIR "/src/Arcstone.Demo.Client"
RUN dotnet build "Arcstone.Demo.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Arcstone.Demo.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Arcstone.Demo.Client.dll"]