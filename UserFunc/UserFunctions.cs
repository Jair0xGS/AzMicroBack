using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserFunc.Svc;

namespace UserFunc
{
    public class UserFunctions(
        UserSvc userSvc,
        ILogger<UserFunctions> logger)
    {
        [Function("ListUsers")]
        public async Task<IActionResult> List([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult(await userSvc.SimpleList());
        }
        [Function("CreateUser")]
        public async Task<IActionResult> Create([HttpTrigger(AuthorizationLevel.Function,  "post")] HttpRequest req)
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult(await userSvc.SimpleList());
        }
        
        [Function("UpdateUser")]
        public async Task<IActionResult> Update([HttpTrigger(AuthorizationLevel.Function,  "put")] HttpRequest req)
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult(await userSvc.SimpleList());
        }
    }
}
