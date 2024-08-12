# AzMicroBack
Multipurpose Azure microservice backend

## Create new Azure Functions Project

1. Create a new project
```
  func init {ProjectName} --worker-runtime dotnet-isolated --target-framework net8.0
```
2. Cd into the folder<br>
3. Run command to add new function
```
  func new --name {NewFunctionName} --template "HTTP trigger" 
```

## Run project

1. Cd into the project folder<br>
3. Run start command
```
  func start
```