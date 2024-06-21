namespace VentaGalaxy.Entities
{
    public class Cliente : EntityBase
    {
        public string NroDocumento { get; set; } = default!;
        public string NombreCompleto { get; set; } = default!;
        public string? Telefono { get; set; }
        public string Correo { get; set; } = default!;
        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}
