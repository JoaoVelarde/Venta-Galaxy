﻿using VentaGalaxy.Services.Interfaces;
using VentaGalaxy.Shared.Request;

namespace VentaGalaxy.WebService.EndPoints
{
    public static class CategoriaEndPoints
    {
        public static void MapCategortiaEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/Categorias");

            group.MapGet("/", async (ICategoriaService service) => { return Results.Ok(await service.ListAsync()); });

            group.MapGet("/{id:int}", async (ICategoriaService service, int id) =>
            {
                var response = await service.FindByIdAsync(id);
                if (response.Success)
                {
                    return Results.Ok(response);
                }

                return Results.NotFound(response);
            });

            group.MapPost("/", async (ICategoriaService service, CategoriaDtoRequest request) =>
            {
                var response = await service.AddAsync(request);
                return Results.Ok(response);
            });

            group.MapPut("/{id:int}", async (ICategoriaService service, int id, CategoriaDtoRequest request) =>
            {
                var response = await service.UpdateAsync(id, request);

                return Results.Ok(response);
            });

            group.MapDelete("/{id:int}", async (ICategoriaService service, int id) =>
            {
                var response = await service.DeleteAsync(id);
                return Results.Ok(response);
            });
        }
    }
}
