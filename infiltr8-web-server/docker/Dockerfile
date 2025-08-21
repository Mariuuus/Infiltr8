FROM mcr.microsoft.com/dotnet/aspnet:9.0
COPY ./server/bin/Release/net9.0/publish/ /app
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "server.dll"]
EXPOSE 8080
