using System.Text;
using server.Models;
using System.Text.Json;
using BCrypt.Net;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly UserService _userService;

    public AuthMiddleware(RequestDelegate next, UserService userService)
    {
        _next = next;
        _userService = userService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // check if its a post or delete request for the level route
        if (context.Request.Path.StartsWithSegments("/api/level") &&
            (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Delete) ||
            context.Request.Path.StartsWithSegments("/api/user") && (context.Request.Method == HttpMethods.Get)
            )
        {
            // enable in order so the body doesnt get deleted, after it was read by the middleware
            context.Request.EnableBuffering();

            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            var requestBody = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
            var data = JsonSerializer.Deserialize<JsonUser>(requestBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (data == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid login data");
                return;
            }

            var user = await _userService.GetByUsernameAsync(data.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(data.Password, user.Password))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid username or password");
                return;
            }

        }
        await _next(context);
    }
}