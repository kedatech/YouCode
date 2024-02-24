# Utiliza la imagen oficial de .NET SDK 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Ajusta la ruta del COPY para considerar la estructura de directorios correcta
COPY ./YouCode.GUI/YouCode.GUI.csproj ./YouCode.GUI/

# Cambia al directorio del proyecto antes de restaurar
WORKDIR /app/YouCode.GUI
RUN dotnet restore

# Copia el resto de los archivos del proyecto
COPY ./YouCode.GUI/ ./

# Compila la aplicación
RUN dotnet publish -c Release -o out 

# Utiliza la imagen de runtime de ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/YouCode.GUI/out .
EXPOSE 5000
ENTRYPOINT ["dotnet", "YouCode.GUI.dll"]
