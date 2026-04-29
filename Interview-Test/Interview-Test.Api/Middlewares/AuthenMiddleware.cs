using System.Security.Cryptography;
using System.Text;

namespace Interview_Test.Middlewares;

public class AuthenMiddleware : IMiddleware
{
    private readonly string hashedKey;

    public AuthenMiddleware(IConfiguration configuration)
    {
        var xApiKey = configuration["ApiKey"] ?? "";
        hashedKey = HashSha512(xApiKey);
    }

    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var apiKeyHeader = context.Request.Headers["x-api-key"];
        if (string.IsNullOrEmpty(apiKeyHeader))
        {
            context.Response.StatusCode = 401;
            return context.Response.WriteAsync("API Key is missing");
        }
        var headerHash = HashSha512(apiKeyHeader.ToString());
        if (!string.Equals(headerHash, hashedKey, StringComparison.OrdinalIgnoreCase))
        {
            context.Response.StatusCode = 401;
            return context.Response.WriteAsync("Unauthorized");
        }
        return next(context);
    }

    private string HashSha512(string input)
    {
        using (SHA512 sha512 = SHA512.Create())
        {
            var hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(hashedBytes);
        }
    }
}