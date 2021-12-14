FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app
COPY . .

ENV SERVER1=172.17.0.2:8080
ENV SERVER=localhost:8080


ENTRYPOINT ["dotnet", "/app/clientapp.dll"]