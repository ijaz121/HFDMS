using CommonServices.ListConverter;
using DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class ActivityLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IListConverter _listConverter;
    private readonly IDbConnectionLogic _dbConnectionLogic;

    public ActivityLogMiddleware(RequestDelegate next, IListConverter listConverter, IDbConnectionLogic dbConnectionLogic)
    {
        _next = next;
        _listConverter = listConverter;
        _dbConnectionLogic = dbConnectionLogic;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Exclude specific endpoint from logging
        if (context.Request.Path.ToString().Contains("/api/ActivityLog/GetActivityLogData"))
        {
            // Skip logging for this endpoint and proceed to the next middleware
            await _next(context);
            return;
        }

        var user = context.User?.Identity?.Name ?? "0";
        var requestTime = DateTime.UtcNow;

        // Capture request details
        var requestBody = await FormatRequestBody(context.Request);

        // Temporarily replace the response body with a memory stream
        var originalResponseBody = context.Response.Body;
        using (var memoryStream = new MemoryStream())
        {
            context.Response.Body = memoryStream;

            // Proceed to the next middleware
            await _next(context);

            // Capture response details
            var responseTime = DateTime.UtcNow;
            var statusCode = context.Response.StatusCode;
            var responseBody = await FormatResponseBody(memoryStream);

            // Log request and response details
            string spName = "SP_InsertActivityLog";
            Hashtable Param = new Hashtable
        {
            { "@UserID", user },  // Ensure UserID is passed as BIGINT
            { "@Action", context.Request.Method },
            { "@Endpoint", context.Request.Path.ToString() },
            { "@Method", context.Request.Method },
            { "@RequestData", requestBody },
            { "@ResponseData", responseBody },
            { "@StatusCode", statusCode.ToString() }
        };

            _dbConnectionLogic.IUD(spName, Param);

            // Reset the memory stream and copy it to the original response body
            memoryStream.Seek(0, SeekOrigin.Begin);
            await memoryStream.CopyToAsync(originalResponseBody);
        }

        // Restore the original response body
        context.Response.Body = originalResponseBody;
    }

    private async Task<string> FormatRequestBody(HttpRequest request)
    {
        // Enable buffering so that we can read the request body multiple times
        request.EnableBuffering();

        if (request.ContentLength == null || request.ContentLength == 0)
        {
            return string.Empty;
        }

        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await request.Body.ReadAsync(buffer, 0, buffer.Length);
        var bodyAsText = Encoding.UTF8.GetString(buffer);

        // Reset the request body stream position so it can be read again later
        request.Body.Position = 0;

        return bodyAsText;
    }

    private async Task<string> FormatResponseBody(Stream responseBodyStream)
    {
        responseBodyStream.Seek(0, SeekOrigin.Begin);
        var text = await new StreamReader(responseBodyStream).ReadToEndAsync();
        responseBodyStream.Seek(0, SeekOrigin.Begin);
        return text;
    }
}
