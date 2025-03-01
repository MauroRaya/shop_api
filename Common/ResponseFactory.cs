using Microsoft.AspNetCore.Mvc;

namespace shop_api.Common;

public class ResponseFactory
{
    public static ICustomResult Failure(int status, string errorMessage)
    {
        var problemDetails = new ProblemDetails
        {
            Status = status,
            Detail = errorMessage,
        };
        
        return new ProblemDetailsResponse(problemDetails);
    }
    
    public static ICustomResult Success<T>(T data)
    {
        return new GenericResponse<T>(data);
    }
}