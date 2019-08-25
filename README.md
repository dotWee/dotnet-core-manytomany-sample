# dotnet-core-manytomany-sample

Many to many sample with entity framework core and dotnet core with support for lazy loading

## usage

1. Create initial entity framework migration: `dotnet ef migrations add InitialCreate`
2. Update local sqlite database: `dotnet ef database update`
3. Run application to test and seed database: `dotnet run`
