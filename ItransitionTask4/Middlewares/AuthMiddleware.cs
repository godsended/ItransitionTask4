using ItransitionTask4.Models;
using ItransitionTask4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ItransitionTask4.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate next;

    public AuthMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context, IAuth auth)
    {
        string? key = context.Request.Cookies["LoginKey"];
        string? email = context.Request.Cookies["Email"];

        await context.SignOutAsync();
        if (key != null && email != null && await auth.TryAutoLogin(email, key))
        {
            await auth.Authenticate(email);
        }
        else
        {
            context.Response.Cookies.Delete("LoginKey");
            context.Response.Cookies.Delete("Email");
        }
        
        await next(context);
    }
}

public static class AuthMiddlewareExtension
{
    public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthMiddleware>();
    }
}