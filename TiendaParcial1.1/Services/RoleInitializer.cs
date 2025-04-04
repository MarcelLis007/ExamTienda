using Microsoft.AspNetCore.Identity;
using TiendaParcial1._1.Models;

namespace TiendaParcial1._1.Services
{
    public class RoleInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            // Obtener el RoleManager desde el contenedor de servicios
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Lista de roles que deben existir
            string[] roleNames = { "Administrador", "Proveedor", "Usuario" };

            // Verificar y crear cada rol si no existe
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}