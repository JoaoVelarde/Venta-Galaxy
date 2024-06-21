using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentaGalaxy.Entities;

namespace VentaGalaxy.DataAccess.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {

            builder.Property(p => p.FechaCrea)
                .HasColumnType("DATE");

            builder.HasQueryFilter(p => p.Estado);
            builder.Property(p => p.NroDocumento)
                .HasMaxLength(12);
            builder.HasIndex(p => p.NroDocumento);


        }

    }
}
