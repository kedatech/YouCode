# Utiliza la imagen oficial de .NET SDK 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Instala Node.js y limpia en un solo paso para reducir la cantidad de capas
RUN apt-get update -yq \
    && apt-get install -yq curl gnupg \
    && curl -sL https://deb.nodesource.com/setup_18.x | bash - \
    && apt-get install -yq nodejs \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Verifica la instalación de Node.js y npm, actualiza npm
RUN npm install -g npm@latest

# Ajusta la ruta del COPY para considerar la estructura de directorios correcta y copia el archivo csproj
COPY ./YouCode.GUI/YouCode.GUI.csproj ./YouCode.GUI/

# Cambia al directorio del proyecto antes de restaurar
WORKDIR /app/YouCode.GUI
RUN dotnet restore

# Copia el resto de los archivos del proyecto
COPY ./YouCode.BE/ ./../YouCode.BE/
COPY ./YouCode.DAL/ ./../YouCode.DAL/
COPY ./YouCode.BL/ ./../YouCode.BL/
COPY ./YouCode.GUI/ .

# Compila la aplicación .NET especificando el proyecto YouCode.GUI
RUN dotnet publish -c Release -o out 

# Utiliza la imagen de runtime de ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/YouCode.GUI/out .
EXPOSE 5000
ENTRYPOINT ["dotnet", "YouCode.GUI.dll"]
