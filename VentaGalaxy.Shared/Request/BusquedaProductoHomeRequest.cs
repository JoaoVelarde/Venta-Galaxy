namespace VentaGalaxy.Shared.Request;

public class BusquedaProductoHomeRequest : RequestBase
{
    public string? Nombre { get; set; }
    public int? CategoriaId { get; set; }
}