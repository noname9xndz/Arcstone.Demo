# required
  - .net 5.0
  - sqlserver
  - Visual Studio or Visual Studio Code

# change appsetings.json
```
"ConnectionStrings": {
    "sqlServer": "Server=.;Database=Arcstone.Demo;User Id =;Password =;MultipleActiveResultSets=true"
  }
```

# build all project
  - dotnet build

# Update-Database
  - EntityFrameworkCore\Update-Database

# Multiple startup projects
 1.With Visual Studio 
  - Arcstone.Demo.Api
  - Arcstone.Demo.Client
 2.Visual Studio Code
  - dotnet run
     + Arcstone.Demo.Api
     + Arcstone.Demo.Client
# client : https://localhost:5001/
# api : https://localhost:5005/swagger/index.html

# Account Login
  + acc : manager@gmail.com or employee@gmail.com
  + password : 123123

