using Microsoft.AspNetCore.Mvc;

namespace shop_api.Common;

public class ProblemDetailsResponse : ICustomResult
{
    private readonly ProblemDetails _problemDetails;

    public ProblemDetailsResponse(ProblemDetails problemDetails)
    {
        _problemDetails = problemDetails;
    }

    public object GetResult()
    {
        return _problemDetails;
    }
}