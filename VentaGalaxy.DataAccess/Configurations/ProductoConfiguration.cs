using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentaGalaxy.Entities;

namespace VentaGalaxy.DataAccess.Configurations
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
           
            builder.Property(p => p.FechaCrea)
                .HasColumnType("DATE");

            builder.Property(p => p.Descripcion)
                .HasMaxLength(500);

            builder.HasQueryFilter(p => p.Estado);

            builder.Property(p => p.PrecioCompra)
                .HasColumnType("decimal(12, 2)");

            builder.Property(p => p.PrecioVenta)
                .HasColumnType("decimal(12, 2)");
            builder.Property(p => p.Url)
                .HasMaxLength(500);
        }
    }
}
