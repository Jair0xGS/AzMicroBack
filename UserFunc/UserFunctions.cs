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
        [Function("UserFunctions")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult(userSvc.SimpleList());
        }
    }
}
