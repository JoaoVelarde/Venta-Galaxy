using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentaGalaxy.Entities;

namespace VentaGalaxy.DataAccess.Configurations
{
    public class VentaDetalleConfiguration : IEntityTypeConfiguration<VentaDetalle>
    {
        public void Configure(EntityTypeBuilder<VentaDetalle> builder)
        {
            builder.HasQueryFilter(p => p.Estado);

            builder.Property(p => p.Precio)
                .HasColumnType("decimal(12, 2)");
        }
    }
}
