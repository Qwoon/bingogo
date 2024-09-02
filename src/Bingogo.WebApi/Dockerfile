# base layer
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /usr/src

# arguments
ARG CONFIGURATION="Release"

RUN chown root:root -R /usr/src

# copy common files
COPY ./nuget.config ./*.props ./src/*.sln ./

# copy library files
COPY src/*/*.csproj ./src/
RUN for file in $(ls ./src/*.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done

# restore .NET dependencies
RUN --mount=type=secret,id=nuget,dst=/usr/nuget.config \
    dotnet restore "src/Bingogo.WebApi";

# copy remaining files
COPY . .

# build
RUN dotnet build "src/Bingogo.WebApi" -c $CONFIGURATION --no-restore

# publish
RUN dotnet publish "src/Bingogo.WebApi" -c $CONFIGURATION -o /srv --no-build --no-restore

# runtime 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
COPY --from=base /srv /srv
ENTRYPOINT [ "dotnet", "srv/Bingogo.WebApi.dll" ]
