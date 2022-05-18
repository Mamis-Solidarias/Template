FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine  AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS publish
WORKDIR /src
COPY ["MamisSolidarias.WebAPI.TEMPLATE/MamisSolidarias.WebAPI.TEMPLATE.csproj", "MamisSolidarias.WebAPI.TEMPLATE/"]
RUN dotnet restore "MamisSolidarias.WebAPI.TEMPLATE/MamisSolidarias.WebAPI.TEMPLATE.csproj"
COPY . .
WORKDIR "/src/MamisSolidarias.WebAPI.TEMPLATE"
RUN dotnet publish "MamisSolidarias.WebAPI.TEMPLATE.csproj" -c Release -o /app/publish \
                                                                   --runtime alpine-x64 \
                                                                   --self-contained true \
                                                                   /p:PublishTrimmed=true \
                                                                   /p:PublishSingleFile=true 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./MamisSolidarias.WebAPI.TEMPLATE"]
