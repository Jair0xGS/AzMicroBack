using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserFunc.Model;
using UserFunc.Svc;

namespace UserFunc;

public class UserFunctions(
    UserSvc userSvc,
    ILogger<UserFunctions> logger)
{
    [Function("ListUsers")]
    public async Task<IActionResult> List([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
    {
        try
        {
            logger.LogInformation("ListUsers http trigger");
            var rs =await userSvc.SimpleList();
            return new OkObjectResult(rs);
        }catch(Exception ex){
            Console.WriteLine(ex);
            return new OkObjectResult("error");
        }
    }

    [Function("CreateUser")]
    public async Task<IActionResult> Create([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        logger.LogInformation("Create User http trigger");
        var newUser = await ExtractFromBody<User>(req);
        if (newUser is null || string.IsNullOrEmpty(newUser.Id))
        {
            return new BadRequestObjectResult("Invalid user data.");
        }
        await userSvc.Create(newUser);
        return new OkObjectResult("success");
    }

    [Function("UpdateUser")]
    public async Task<IActionResult> Update([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequest req)
    {
        logger.LogInformation("UpdateUser http trigger");
        var newUser = await ExtractFromBody<User>(req);
        if (newUser is null || string.IsNullOrEmpty(newUser.Id))
        {
            return new BadRequestObjectResult("Invalid user data.");
        }
        await userSvc.Update(newUser);
        return new OkObjectResult("success");
    }

    [Function("DeleteUser")]
    public async Task<IActionResult> DeleteUser(
        [HttpTrigger(
            AuthorizationLevel.Function, 
            "delete",
            Route = "DeleteUser/{id}"
            )
        ] 
        HttpRequest req,
        string id
        )
    {
        logger.LogInformation("UpdateUser http trigger");
        if (string.IsNullOrEmpty(id))
        {
            return new BadRequestObjectResult("User id is required.");
        }

        await userSvc.Delete(id);
        return new OkObjectResult("success");
    }

    private static async Task<T?> ExtractFromBody<T>(HttpRequest req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        return JsonConvert.DeserializeObject<T>(requestBody);
    }
}