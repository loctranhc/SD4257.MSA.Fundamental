FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5003
EXPOSE 15003

RUN mkdir -p startup
COPY ./aspnet/https/update-ca-certificate-order.sh /startup/
RUN chmod +x /startup/update-ca-certificate-order.sh

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./MSA.OrderService/MSA.OrderService.csproj","src/MSA.OrderService/"]
COPY ["./MSA.Common.Contracts/MSA.Common.Contracts.csproj","src/MSA.Common.Contracts/"]
COPY ["./MSA.Common.PostgresMassTransit/MSA.Common.PostgresMassTransit.csproj","src/MSA.Common.PostgresMassTransit/"]
RUN dotnet restore "src/MSA.OrderService/MSA.OrderService.csproj"
COPY . .
WORKDIR "/src/MSA.OrderService"
RUN dotnet build "MSA.OrderService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MSA.OrderService.csproj" -c Release -o /app/publish --self-contained false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["/startup/update-ca-certificate-order.sh"]