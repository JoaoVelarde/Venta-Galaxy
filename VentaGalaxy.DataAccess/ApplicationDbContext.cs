using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaGalaxy.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ClienteIdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ClienteIdentityUser>(e =>
            {
                e.ToTable("Usuario");
                e.Property(p => p.LockoutEnd).HasColumnName("BloqueoHasta");
            });
            builder.Entity<IdentityRole>(e => e.ToTable("Rol"));
            builder.Entity<IdentityUserRole<string>>(e => e.ToTable("UsuarioRol"));
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<string>()
                .HaveMaxLength(150);

            configurationBuilder.Conventions.Remove<CascadeDeleteConvention>();
        }

    }
}
