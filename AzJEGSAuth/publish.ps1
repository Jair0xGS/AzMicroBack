cd AzFunctions 
dotnet clean 
Remove-Item -Path ./bin -Recurse -Force
Remove-Item -Path ./obj -Recurse -Force
func azure functionapp publish JegsAuthFn
cd ..