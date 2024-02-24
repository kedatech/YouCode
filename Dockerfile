# Utiliza la imagen oficial de .NET SDK 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Instala Node.js
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_16.x | bash \
    && apt-get install nodejs -yq

# Verifica la instalación de Node.js y npm
RUN node --version
RUN npm --version

# Ajusta la ruta del COPY para considerar la estructura de directorios correcta
COPY ./YouCode.GUI/YouCode.GUI.csproj ./YouCode.GUI/

# Cambia al directorio del proyecto antes de restaurar
WORKDIR /app/YouCode.GUI
RUN dotnet restore

# Copia el resto de los archivos del proyecto
COPY ./YouCode.GUI/ ./

# Instala las dependencias de Node.js, por ejemplo, para TailwindCSS
RUN npm install

# Compila la aplicación .NET
RUN dotnet publish -c Release -o out 

# Utiliza la imagen de runtime de ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/YouCode.GUI/out .
EXPOSE 5000
ENTRYPOINT ["dotnet", "YouCode.GUI.dll"]
