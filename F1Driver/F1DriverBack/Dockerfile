#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["F1DriverBack/F1DriverBack.csproj", "F1DriverBack/"]
COPY ["ModelLayer/ModelLayer.csproj", "ModelLayer/"]
COPY ["DataLayer/DataLayer.csproj", "DataLayer/"]
RUN dotnet restore "F1DriverBack/F1DriverBack.csproj"
COPY . .
WORKDIR "/src/F1DriverBack"
RUN dotnet build "F1DriverBack.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "F1DriverBack.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "F1DriverBack.dll"]