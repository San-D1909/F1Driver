#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["F1Driver-Front/F1Driver-Front.csproj", "F1Driver-Front/"]
RUN dotnet restore "F1Driver-Front/F1Driver-Front.csproj"
COPY . .
WORKDIR "/src/F1Driver-Front"
RUN dotnet build "F1Driver-Front.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "F1Driver-Front.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "F1Driver-Front.dll"]