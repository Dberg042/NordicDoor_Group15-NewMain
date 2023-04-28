#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NordicDoor_Group15/NordicDoor_Group15.csproj", "NordicDoor_Group15/"]
RUN dotnet restore "NordicDoor_Group15/NordicDoor_Group15.csproj"
COPY . .
WORKDIR "/src/NordicDoor_Group15"
RUN dotnet build "NordicDoor_Group15.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NordicDoor_Group15.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NordicDoor_Group15.dll"]