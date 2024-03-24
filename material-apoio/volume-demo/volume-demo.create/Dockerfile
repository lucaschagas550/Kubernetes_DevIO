#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["volume-demo.create/VolumeDemo.CreateFiles.csproj", "volume-demo.create/"]
RUN dotnet restore "volume-demo.create/VolumeDemo.CreateFiles.csproj"
COPY . .
WORKDIR "/src/volume-demo.create"
RUN dotnet build "VolumeDemo.CreateFiles.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VolumeDemo.CreateFiles.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VolumeDemo.CreateFiles.dll"]