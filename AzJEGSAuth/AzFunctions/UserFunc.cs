using System.Text.Json;
using AzFunctions.DTOs;
using Domain.DTOs;
using Domain.Models;
using Domain.UseCases;
using ErrorOr;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzFunctions
{
    public class UserFunc(
        ILogger<UserFunc> logger,
        UserUc userUc
    ) : BApi
    {
        [Function("UserList")]
        public async Task<IActionResult> List([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            var rs = await userUc.List();
            return Oki(rs);
        }

        [Function("UserCreate")]
        public async Task<IActionResult> Create([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            var cmd = await Bind<UserCreateCommand>(req);
            if (cmd.IsError) return Proc(cmd);
            var ts = await userUc.Create(cmd.Value);
            return Proc(ts);
        }

        [Function("UserGet")]
        public async Task<IActionResult> Get([ HttpTrigger(AuthorizationLevel.Anonymous, "get", Route="UserGet/{id}" )] HttpRequest req, Guid id)
        {
            var rs = await userUc.Get(id);
            return Proc(rs);
        }


        [Function("UserUpdate")]
        public async Task<IActionResult> Update([ HttpTrigger( AuthorizationLevel.Anonymous, "put" )] HttpRequest req, Guid id) {
            var cmd = await Bind<UserUpdateCommand>(req);
            if (cmd.IsError) return Proc(cmd);
            var rs = await userUc.Update(cmd.Value);
            return Proc(rs);
        }

        [Function("UserDelete")]
        public async Task<IActionResult> Delete([HttpTrigger( AuthorizationLevel.Anonymous, "delete", Route="UserDelete/{id}" )] HttpRequest req,
            Guid id)
        {
            var td = await userUc.Delete(id);
            return Proc(td);
        }
    }
}