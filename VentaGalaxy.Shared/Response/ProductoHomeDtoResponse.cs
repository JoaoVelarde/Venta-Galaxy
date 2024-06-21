namespace VentaGalaxy.Shared.Response;

public class ProductoHomeDtoResponse
{
    public int Id { get; set; }
    public string? Producto { get; set; }
    public string? Descripcion { get; set; }
    public string? Categoria { get; set; }
    public decimal PrecioVenta { get; set; }
    public int? Cantidad { get; set; }
    public string? Url { get; set; }
}