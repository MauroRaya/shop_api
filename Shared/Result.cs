using Microsoft.AspNetCore.Mvc;

namespace shop_api.Common;

public readonly struct Result<TData, TError>
{
    public readonly TData? Data { get; }
    public readonly TError? Error { get; }

    public bool IsSuccess { get; }
    
    public Result(TData data)
    {
        IsSuccess = true;
        Data = data;
        Error = default;
    }
    
    public Result(TError error)
    {
        IsSuccess = false;
        Error = error;
        Data = default;
    }
    
    public static implicit operator Result<TData, TError>(TData data) => new(data); 
    public static implicit operator Result<TData, TError>(TError error) => new(error);

    public IActionResult Match(
        Func<TData, IActionResult> success,
        Func<TError, IActionResult> failure)
    {
        return IsSuccess
            ? success(Data!)
            : failure(Error!);
    }
}