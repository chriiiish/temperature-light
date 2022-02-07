FROM public.ecr.aws/lambda/dotnet:core3.1 as base

FROM mcr.microsoft.com/dotnet/sdk:3.1 as build-image
WORKDIR /app

COPY *.csproj . 
RUN dotnet restore

COPY . .
RUN dotnet publish --no-restore -c Release -o /publish

FROM base AS final
WORKDIR /var/task
COPY --from=build-image /publish .
CMD ["Lambdas::Lambdas.Default::Handle"]