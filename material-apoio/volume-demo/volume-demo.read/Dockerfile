#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["volume-demo.read/VolumeDemo.ListFiles.csproj", "volume-demo.read/"]
RUN dotnet restore "volume-demo.read/VolumeDemo.ListFiles.csproj"
COPY . .
WORKDIR "/src/volume-demo.read"
RUN dotnet build "VolumeDemo.ListFiles.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VolumeDemo.ListFiles.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VolumeDemo.ListFiles.dll"]