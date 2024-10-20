using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzFunctions.DTOs;

public class BApi
{
    protected BadRequestObjectResult Err<T>(ErrorOr<T> errorOr)
    {
        return Err(errorOr.FirstError.Code, errorOr.FirstError.Description);
    }

    protected BadRequestObjectResult Err(string description)
    {
        return Err("exception", description);
    }
    
    protected BadRequestObjectResult Err(Error err)
    {
        return Err(err.Code, err.Description);
    }
    
    protected BadRequestObjectResult BadRequest()
    {
        return Err("exception","Bad request");
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

    protected IActionResult Proc<T>(ErrorOr<T> rs)
    {
        if (!rs.IsError) return Oki(rs.Value);
        if (rs.Errors.All(el => el.Type == ErrorType.Validation))
        {
                return new BadRequestObjectResult(
                    new ApiResponse(
                        false, 
                        "400", 
                        null,
                        new ErrorResponseData(
                            "validation", 
                            "Errores de validacion",
                            rs.Errors.Select(e=>new ErrorResponseElement(e.Code,e.Description))
                            )));
        }
        return Err(rs.FirstError);
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
    }
    protected static async Task<ErrorOr<T>> Bind<T>(HttpRequest req)
    {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var result= JsonSerializer.Deserialize<T>(requestBody);
            if (result is null) return Error.Failure("error", "bad request");
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(result);
            var isValid = Validator.TryValidateObject(result, context, validationResults, true);
            Console.WriteLine("validation result ? : {0}",isValid);
            if (isValid) return result;
            List<Error> errors = [];
            Console.WriteLine(JsonSerializer.Serialize(validationResults));
            foreach (var validationResult in validationResults)
            {
                errors.Add(Error.Validation(
                    validationResult.MemberNames.FirstOrDefault()??"",
                    validationResult.ErrorMessage??""
                ));
            }
            return errors;
    }
}