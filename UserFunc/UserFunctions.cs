using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserFunc
{
    public class UserFunctions
    {
        private readonly ILogger<UserFunctions> _logger;

        public UserFunctions(ILogger<UserFunctions> logger)
        {
            _logger = logger;
        }

        [Function("UserFunctions")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
