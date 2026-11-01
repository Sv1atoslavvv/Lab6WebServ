using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace EmailSenderWeb.Logging;

public sealed class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requestTime = DateTimeOffset.Now;
        var requestUrl = context.Request.GetDisplayUrl();
        var remoteIp = context.Connection.RemoteIpAddress;
        var ipAddress = remoteIp is null
            ? "unknown"
            : remoteIp.IsIPv4MappedToIPv6
                ? remoteIp.MapToIPv4().ToString()
                : remoteIp.ToString();

        _logger.LogInformation(
            "Request URL: {RequestUrl} | Time: {RequestTime:yyyy-MM-dd HH:mm:ss.fff zzz} | IP: {IpAddress}",
            requestUrl,
            requestTime,
            ipAddress);

        await _next(context);
    }
}
