using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            logger.LogInformation("C# HTTP trigger function processed a request.");
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
        logger.LogInformation("C# HTTP trigger function processed a request.");
        await userSvc.Create(new User
        {
            UserId = Guid.NewGuid().ToString(),
            RoleId = "",
            UserName = "jose",
            Name = "dd",
            LastName = "dfff",
            DocumentType = "ddd",
            DocumentNumber = "ddd",
            Gender = "aaa",
            PhoneNumber = "ssdasd",
            Enabled = true,
            EmailVerificationDate = DateTime.Now,
            ResetToken = null,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        });
        return new OkObjectResult("success");
    }

    [Function("UpdateUser")]
    public async Task<IActionResult> Update([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequest req)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult(await userSvc.SimpleList());
    }
}