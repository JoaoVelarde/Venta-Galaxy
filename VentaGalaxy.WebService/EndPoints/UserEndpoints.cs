using Microsoft.AspNetCore.WebUtilities;
using VentaGalaxy.Services.Interfaces;
using VentaGalaxy.Shared.Request;

namespace VentaGalaxy.WebService.EndPoints;

public static class UserEndpoints
{

    public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("api/Users");

        group.MapPost("/login", async (IUserService service, LoginDtoRequest request) =>
        {
            var response = await service.LoginAsync(request);
            return Results.Ok(response);
        });

        group.MapPost("/register", async (IUserService service, RegistrarUsuarioDto request) =>
        {
            var response = await service.RegisterAsync(request);
            return Results.Ok(response);
        });

        group.MapPost("/sendTokenToResetPassword",
            async (IUserService service, GenerateTokenToResetDtoRequest request) =>
            {
                var response = await service.SendTokenToResetPasswordAsync(request);
                return Results.Ok(response);
            });

        group.MapPost("/resetPassword", async (IUserService service, ResetPasswordDtoRequest request) =>
        {
            var response = await service.ResetPasswordAsync(request);
            return Results.Ok(response);
        });
    }
}