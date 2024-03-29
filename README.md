# pimi-connect-api
[![UnitTests](https://github.com/grupadotnet/pimi-connect-api/actions/workflows/unit_tests.yml/badge.svg)](https://github.com/grupadotnet/pimi-connect-api/actions/workflows/unit_tests.yml)

## Project Description
Link to project diagram:
https://www.figma.com/file/XQxeE5fQXhJE8QH4qGUTdu/PiMI-Connect?type=whiteboard&node-id=0-1&t=LbNPEUyWQQIx3t2h-0

## How to Install and Run?

### Install:
- Download and install [**.NET 8**](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (take a note that if you are using Visual Studio, then it has to be in version 2022, otherwise .net sdk might not work properly),
- Install [**Entity Framework Core**](https://learn.microsoft.com/en-us/ef/core/cli/dotnet),
- Download and install [**PostgreSQL**](https://www.postgresql.org/download/).



### Additional installations:
- Use whatever code editor you like for example: [**Visual Studio**](https://visualstudio.microsoft.com/downloads/), [**Visual Studio Code**](https://code.visualstudio.com/Download), [**Rider**](https://www.jetbrains.com/rider/download/#section=windows) etc.,
- For sending requests to API you can download and install [**Postman**](https://www.postman.com/downloads/),
- For browsing the database you can download and install [**DBeaver**](https://dbeaver.io/download/).



### Run:
- Clone repository:

```bash
git clone https://github.com/grupadotnet/pimi-connect-api.git
cd pimi-connect-api/pimi-connect-api/pimi-connect-api.API/
```

- In `pimi-connect-api.API/` directory create the following files:

**appsettings.json**

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "pimi-connect-postgresql": "Host=localhost; Port=5432; Database=pimi-connect; Username=postgres; Password=password123"
  }
}
```

**appsettings.Tests.json**

```json
{
  "ConnectionStrings": {
    "pimi-connect-postgresql-test": "Host=localhost; Port=5432; Database=pimi-connect-test; Username=postgres; Password=password123"
  },
  "TestSettings": {
    "EntitiesCount": 6,
    "ConnectionStringName": "pimi-connect-postgresql-test",
    "MigrationsAssemblyName": "pimi-connect-api.API"
  } 
}
```

You can also copy example files from root directory to ./pimi-connect-api/pimi-connect-api.API
- In project's root directory run:
```bash
cp appsettings_example.json ./pimi-connect-api/pimi-connect-api.API/appsettings.json
cp appsettings_example.Tests.json ./pimi-connect-api/pimi-connect-api.API/appsettings.Tests.json
```

Change the `ConnectionStrings` according to your PostgreSQL configuration.

- Create databases (`/pimi-connect-api/pimi-connect-api.API`):

```bash
dotnet ef database update 
dotnet ef database update --configuration Tests 
```

- Run the project:

```bash
dotnet run
```

# Running postgres with docker
You can alternatively run PostgreSQL with docker. 

- Run with command 
```bash
docker run --name pimi-connect-postgresql \
-p 5432:5432 \
-e POSTGRES_PASSWORD=password123 \
-d postgres:latest
```

- Run with docker compose - in project's root directory run
```bash
docker compose up -d
```


