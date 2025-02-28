using System.Net;

namespace shop_api.API.Common;

public class Result<T>
{
    public int StatusCode;
    public bool IsSuccess;
    public string Message = string.Empty;
    public T? Data;

    private Result<T> SetStatusCode(int statusCode)
    {
        StatusCode = statusCode;
        return this;
    }
    
    private Result<T> SetIsSuccess(bool isSuccess)
    {
        IsSuccess = isSuccess;
        return this;
    }
    
    private Result<T> SetMessage(string message)
    {
        Message = message;
        return this;
    }

    private Result<T> SetData(T? data)
    {
        Data = data;
        return this;
    }

    public static Result<T> Success(int statusCode, string message, T data)
        => new Result<T>()
            .SetStatusCode(statusCode)
            .SetMessage(message)
            .SetIsSuccess(true)
            .SetData(data);
    
    
    public static Result<T> Failure(int statusCode, string message) 
        => new Result<T>()
            .SetStatusCode(statusCode)
            .SetMessage(message)
            .SetIsSuccess(false)
            .SetData(default);
}