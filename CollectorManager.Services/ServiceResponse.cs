using System.Net;

namespace CollectorManager.Services;

public class ServiceResponse<T> : ServiceResponse
{
    public ServiceResponse(HttpStatusCode statusCode)
        : base(statusCode)
    {
    }

    public ServiceResponse(T data)
        : this(HttpStatusCode.OK)
    {
        Data = data;
    }

    public ServiceResponse(T data, HttpStatusCode statusCode)
        : base(statusCode)
    {
        Data = data;
    }

    public T? Data { get; }
}

public class ServiceResponse
{
    public ServiceResponse(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; }

    public bool IsSuccess => StatusCode >= HttpStatusCode.OK && StatusCode < HttpStatusCode.Ambiguous;
}
