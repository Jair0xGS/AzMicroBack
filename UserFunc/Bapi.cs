using System.Text.Json;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserFunc.Model;

namespace UserFunc;

public class Bapi
{
    protected BadRequestObjectResult Err<T>(ErrorOr<T> errorOr)
    {
        return Err(errorOr.FirstError.Code, errorOr.FirstError.Description);
    }
    protected BadRequestObjectResult Err(string description)
    {
        return Err("exception",description);
    }
    
    protected BadRequestObjectResult Err(string code, string description)
    {
        return new BadRequestObjectResult(new ApiResponse(false, "400", null,
            new ErrorResponseData(code, description, [])));
    }

    protected ObjectResult Oki(object? item)
    {
        return new OkObjectResult(new ApiResponse(true, "200", item, null));
    }

    protected ObjectResult Proc(ErrorOr<object?> item)
    {
        return item.IsError ? Err(item) : Oki(item.Value);
    }

    protected static async Task<T?> GetValue<T>(HttpRequest req)
    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonSerializer.Deserialize<T>(requestBody);
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return default;
        }
    }

    protected static async Task<List<T>> GetValues<T>(HttpRequest req)
    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonSerializer.Deserialize<IEnumerable<T>>(requestBody);
            return data != null ? data.ToList() : [];
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return [];
        }
    }}