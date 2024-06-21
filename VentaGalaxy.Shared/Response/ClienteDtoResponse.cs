namespace VentaGalaxy.Shared.Response
{
    public class ClienteDtoResponse
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = default!;
        public string NroDocumento { get; set; } = default!;
        public string? Telefono { get; set; }
        public string Correo { get; set; } = default!;
    }
}
