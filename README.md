# Steps To Configure Azure

### Create resource Group
```shell
az group create --name JegsRg --location eastus
```
### Create Storage Account
```shell
az storage account create --name jegssa --location eastus --resource-group JegsRg --sku Standard_LRS --allow-blob-public-access false
```
### Create resource Group
```shell
az functionapp create --resource-group JegsRg --consumption-plan-location eastus --runtime dotnet-isolated --functions-version 4 --name JegsAuthFn --storage-account jegssa
```

### Create resource Group
```shell
az functionapp create --resource-group JegsRg --consumption-plan-location eastus --runtime dotnet-isolated --functions-version 4 --name JegsAuthFn --storage-account jegssa
```

### Create resource Group
```shell
func azure functionapp publish JegsAuthFn 
```