using AzFunctions.DTOs;
using Domain.DTOs;
using Domain.UseCases;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzFunctions
{
    public class AuthFunc(
        ILogger<AuthFunc> logger,
        AuthUc authUc
        ):BApi
    {
        [Function("Login")]
        public async Task<IActionResult> Login([HttpTrigger(AuthorizationLevel.Anonymous,  "post")] HttpRequest req)
        {
            var cmd = await Bind<LoginCommand>(req);
            if (cmd.IsError) return Proc(cmd);
            var rs= await authUc.Login(cmd.Value);
            return Proc(rs);
        }
        
        [Function("Logout")]
        public async Task<IActionResult> Logout([HttpTrigger(AuthorizationLevel.Anonymous,  "post")] HttpRequest req)
        {
            var cmd = await Bind<LogoutCommand>(req);
            if (cmd.IsError) return Proc(cmd);
            var rs= await authUc.Logout(cmd.Value);
            return Proc(rs);
        }
        
        
        [Function("Refresh")]
        public async Task<IActionResult> Refresh([HttpTrigger(AuthorizationLevel.Anonymous,  "post")] HttpRequest req)
        {
            var cmd = await Bind<RefreshCommand>(req);
            if (cmd.IsError) return Proc(cmd);
            var rs= await authUc.RefreshToken(cmd.Value);
            return Proc(rs);
        }
    }
}
