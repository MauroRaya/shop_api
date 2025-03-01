using Microsoft.AspNetCore.Mvc;

namespace shop_api.Common;

public class GenericResponse<T> : ICustomResult
{
    private readonly T _data;

    public GenericResponse(T data)
    {
        _data = data;
    }

    public object GetResult()
    {
        return _data;
    }
}