
using VentaGalaxy.Services.Interfaces;

namespace VentaGalaxy.WebService.EndPoints;

public static class ReporteVentaEndpoints
{
    public static void MapReportes(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("api/ReportesVenta");

        //group.MapGet("/VentaPorMes/{anio:int}", async (IVentaDetalleService service, int anio) =>
        //{
        //    var response = await service.ReporteTalleresPorMes(anio);
        //    return Results.Ok(response);
        //});

        //group.MapGet("/VentaPorMontos/{anio:int}", async (IVentaDetalleService service, int anio) =>
        //{
        //    var response = await service.ReporteTalleresPorInstructor(anio);
        //    return Results.Ok(response);
        //});
    }

}