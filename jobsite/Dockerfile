#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["jobsite/jobsite.csproj", "jobsite/"]
RUN dotnet restore "jobsite/jobsite.csproj"
COPY . .
WORKDIR "/src/jobsite"
RUN dotnet build "jobsite.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "jobsite.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "jobsite.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT ASPNETCORE_HTTPS_PORT=443 dotnet jobsite.dll