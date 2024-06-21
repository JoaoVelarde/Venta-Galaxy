using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaGalaxy.Entities;

namespace VentaGalaxy.DataAccess.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            var fecha = DateTime.Parse("2024-06-01");

            builder.HasData(new List<Categoria>
            {
                new() { Id = 1, Nombre = "Linea Blanca", FechaCrea = fecha},
                new() { Id = 2, Nombre = "Limpieza", FechaCrea = fecha },
                new() { Id = 3, Nombre = "Computo", FechaCrea = fecha },
               
            });
            builder.HasQueryFilter(p => p.Estado);
        }
    }
}
