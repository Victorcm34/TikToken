#Prepare build
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /source
EXPOSE 80
COPY *.csproj .
RUN dotnet restore --use-current-runtime
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
#Build
COPY . .
RUN dotnet ef database update
RUN dotnet build
RUN dotnet publish -c Release -o /app --use-current-runtime --self-contained false --no-restore

#Production
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
COPY --from=build /source/*.db .
COPY --from=build /app .
ENTRYPOINT ["dotnet", "TikToken.dll"]