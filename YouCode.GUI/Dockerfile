# Utiliza la imagen oficial de .NET SDK 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia el archivo del proyecto y restaura las dependencias
COPY YouCode.GUI.csproj ./
RUN dotnet restore

# Copia todo el contenido del proyecto y compila la aplicación
COPY . .
RUN dotnet publish -c Release -o out

# Publica la aplicación en una imagen de ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 5000
ENTRYPOINT ["dotnet", "YouCode.GUI.dll"]