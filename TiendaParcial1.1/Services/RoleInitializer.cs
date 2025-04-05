using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaParcial1._1.Services
{
    public static class RoleInitializer
    {
        // Método estático para inicializar los roles
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider), "El proveedor de servicios no puede ser nulo.");
            }

            try
            {
                // Obtener el RoleManager desde el contenedor de servicios
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Lista de roles que deben existir
                string[] roleNames = { "Administrador", "Proveedor", "Usuario" };

                // Verificar y crear cada rol si no existe
                foreach (var roleName in roleNames)
                {
                    bool roleExists = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExists)
                    {
                        var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                        if (!result.Succeeded)
                        {
                            throw new InvalidOperationException($"No se pudo crear el rol '{roleName}'. Errores: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error interno: {ex.InnerException?.Message}");
                throw new ApplicationException("Error al inicializar los roles.", ex);
            }
        }
    }
}