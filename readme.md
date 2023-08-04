# Money App

## Prerequisite

1. Download latest .NET SDK https://dotnet.microsoft.com/en-us/download
2. Install docker

## Installation

### 1. Setup docker services

```
cd docs/
docker compose up
docker start sqlexpress # run this everytime start coding
```

### 2. Setup api

```
# assume you're in /docs
# back to root
cd ../
cp src/server/MoneyApi/appsettings.json src/server/MoneyApi/appsettings.Development.json

# modify settings if needed

# restore dotnet tools
dotnet tool restore

# apply migration
dotnet ef database update -s src/server/moneyapi
```

### 3. Run the api

```
dotnet run --project src/server/MoneyApi/ --environment Development
```

### migrations

```bash
cd src
# new manifest
dotnet new tool-manifest

# install if not available
# dotnet tool install dotnet-ef

# restore dotnet tool, run once only
dotnet tool restore

# check database
dotnet ef dbcontext info -s src/server/moneyapi

# apply migrations
dotnet ef database update -s src/server/moneyapi

# add new migration
dotnet ef migrations add Init -s src/server/moneyapi/ -p src/server/moneyapi/

# generate a migration script
dotnet ef migrations script -s src/server/moneyapi/ -p src/server/moneyapi/
```
