# Thesis Computer Catalog

This is an implementation of the Developer Interview Task posed by Thesis Cloud. It implements a small web application that catalogs computers, storing components and specifications of each computer in the catalog, and allowing the user to browse, edit and search the catalog.

### Architecture

This application consists of the following projects:

* `ThesisCatalog.Core` - Contains entities and extensions that are used throughout the other projects in the solution
* `ThesisCatalog.API` - The backend portion of the application, stores catalog data in SQL Server and exposes it through a RESTful HTTP API
* `ThesisCatalog.Client` The frontend portion of the application; an asp.net Blazor standalone SPA that consumes the API

### Local Development

For local development, Docker is required, since the `ThesisCatalog.API` project runs in a docker container. It also depends on an SQL Server container image.

The `ThesisCatalog.Client` project is not containerized and can be run directly, however on a local development environment, it assumes `ThesisCatalog.API` is running in a local docker container.

#### Setup

Create an `.env` file at the root level (the same directory as this readme file).

If you are on a Mac, add this to your `.env` file:

```
DOTNET_USERSECRETS_PATH=~/.microsoft/UserSecrets
CERTIFICATE_PATH=~/.aspnet
```

If you are on Windows, add this to your `.env` file:

```
DOTNET_USERSECRETS_PATH=${APPDATA}/Microsoft/UserSecrets
CERTIFICATE_PATH=${USERPROFILE}/.aspnet
```

Run the compose file to install SQL Server and the `ThesisCatalog.API` container:

```
docker compose up --build -d
```

Next, build and run the `ThesisCatalog.Client` project:

```
dotnet build ./ThesisCatalog.Client/
```

The site will be published to the `ThesisCatalog.Client/bin/Debug/net8.0/wwwroot` directory. Open it in a web browser directly, or use a web server to serve it. If you are running Visual Studi, it will automatically use the Blazor dev server at https://localhost:7043

