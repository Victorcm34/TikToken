# FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /source
EXPOSE 80
# copy csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore --use-current-runtime
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
# copy everything else and build app
COPY . .
RUN dotnet build
RUN dotnet ef database update
RUN dotnet publish -c Release -o /app --use-current-runtime --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app

COPY --from=build /app .
COPY TikToken.db /app
ENTRYPOINT ["dotnet", "TikToken.dll"]