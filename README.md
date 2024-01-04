# pimi-connect-api


## Project Description
Link to project diagram:
https://www.figma.com/file/XQxeE5fQXhJE8QH4qGUTdu/PiMI-Connect?type=whiteboard&node-id=0-1&t=LbNPEUyWQQIx3t2h-0

## How to Install and Run?

### Install:
- Download and install [**.NET 8**](https://dotnet.microsoft.com/en-us/download/dotnet/8.0),
- Install [**Entity Framework Core**](https://learn.microsoft.com/en-us/ef/core/cli/dotnet),
- Download and install [**PostgreSQL**](https://www.postgresql.org/download/).



### Additional installations:
- Use whatever code editor you like for example: [**Visual Studio**](https://visualstudio.microsoft.com/downloads/), [**Visual Studio Code**](https://code.visualstudio.com/Download), [**Rider**](https://www.jetbrains.com/rider/download/#section=windows) etc.,
- For sending requests to API you can download and install [**Postman**](https://www.postman.com/downloads/),
- For browsing the database you can download and install [**DBeaver**](https://dbeaver.io/download/).



### Run:
- Clone repository:

```
git clone https://github.com/grupadotnet/pimi-connect-api.git
cd pimi-connect-api/pimi-connect-api/pimi-connect-api.API/
```

- In `pimi-connect-api.API/` directory create the following files:

**appsettings.json**

```
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

```
{
  "ConnectionStrings": {
    "pimi-connect-postgresql-test": "Host=localhost; Port=5432; Database=pimi-connect-test; Username=postgres; Password=password123"
  }
}
```

Change the `ConnectionStrings` according to your PostgreSQL configuration.

- Create databases:

```
dotnet ef database update 
dotnet ef database update --configuration Tests 
```

- Run the project:

```
dotnet run
```


