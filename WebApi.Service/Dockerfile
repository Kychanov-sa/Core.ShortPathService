#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
COPY /WebApi.Service/bin/Release/net6.0/linux-x64 .

EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "ShortPathService.dll"]