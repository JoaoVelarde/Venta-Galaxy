namespace VentaGalaxy.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCrea { get; set; }
        public EntityBase()
        {
            Estado = true;
            FechaCrea = DateTime.UtcNow;
        }
    }
}
