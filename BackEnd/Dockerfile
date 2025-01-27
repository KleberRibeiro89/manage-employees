#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.


# Use a imagem base do .NET 8 SDK para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY *.sln .
COPY ./BackEnd.AppService/BackEnd.AppService.csproj ./BackEnd.AppService/
COPY ./BackEnd.AppService.Application/BackEnd.AppService.Application.csproj ./BackEnd.AppService.Application/
COPY ./BackEnd.Repository/BackEnd.Repository.csproj ./BackEnd.Repository/
COPY ./BackEnd.Test/BackEnd.Test.csproj ./BackEnd.Test/
COPY ./WebApi/WebApi.csproj ./WebApi/
RUN dotnet restore 

COPY . ./

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out ./

EXPOSE 5224
ENV ASPNETCORE_URLS=http://+:5224

ENTRYPOINT ["dotnet", "WebApi.dll"]