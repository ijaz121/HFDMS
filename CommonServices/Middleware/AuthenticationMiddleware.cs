using CommonServices.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonServices.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory; // Use IServiceScopeFactory

        public AuthenticationMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
         {
            _next = next;
            _scopeFactory = scopeFactory;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Bypass authentication for the Login route
            if (context.Request.Path.StartsWithSegments("/api/Auth/Login", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            // Get Authorization token from headers
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized access - Missing or invalid Authorization header");
                return;
            }

            // Get userId from headers
            var userId = context.Request.Headers["userId"].FirstOrDefault();
            if (string.IsNullOrEmpty(userId))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized access - Missing userId");
                return;
            }

            // Create a new scope to resolve the scoped IClaimValues service
            using (var scope = _scopeFactory.CreateScope())
            {
                var claimValues = scope.ServiceProvider.GetRequiredService<IClaimValues>();

                // Validate userId and auth token (JWT)
                var token = authHeader.Split(' ').LastOrDefault();
                if (!claimValues.ValidateUser(userId, token))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized access - Invalid userId or token");
                    return;
                }
            }

            // Proceed to the next middleware if validation passes
            await _next(context);
        }
    }
}
