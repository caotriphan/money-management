# Money App

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
