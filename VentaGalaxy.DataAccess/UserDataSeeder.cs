using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using VentaGalaxy.Shared;

namespace VentaGalaxy.DataAccess;

public static class UserDataSeeder
{
    public static async Task Seed(IServiceProvider service)
    {

        var userManager = service.GetRequiredService<UserManager<ClienteIdentityUser>>();

        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

        var adminRole = new IdentityRole(Constantes.RolAdministrador);
        var clienteRole = new IdentityRole(Constantes.RolCliente);

        if (!await roleManager.RoleExistsAsync(Constantes.RolAdministrador))
        {
            await roleManager.CreateAsync(adminRole);
        }

        if (!await roleManager.RoleExistsAsync(Constantes.RolCliente))
        {
            await roleManager.CreateAsync(clienteRole);
        }

        var adminUser = new ClienteIdentityUser()
        {
            NombreCompleto = "Administrador del Sistema",
            UserName = "admin",
            Email = "admin@gmail.com",
            PhoneNumber = "+1 999 999 999",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, "$$Admin.");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, Constantes.RolAdministrador);
        }
    }
}