#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["pod-lifecycle.running/pod-lifecycle.running.csproj", "pod-lifecycle.running/"]
RUN dotnet restore "pod-lifecycle.running/pod-lifecycle.running.csproj"
COPY . .
WORKDIR "/src/pod-lifecycle.running"
RUN dotnet build "pod-lifecycle.running.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "pod-lifecycle.running.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "pod-lifecycle.running.dll"]