using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TiendaParcial1._1.Models; // Asegúrate de que este using apunte a donde están tus modelos

namespace TiendaParcial1._1.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets para cada una de tus entidades
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<OrdenCompra> OrdenesCompra { get; set; }
    public DbSet<ProductoOrdenCompra> ProductosOrdenCompra { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Importante para la configuración de Identity

        // Configuraciones adicionales para tus modelos

        // Configuración para la relación muchos-a-muchos entre OrdenCompra y Producto
        modelBuilder.Entity<ProductoOrdenCompra>()
            .HasKey(poc => new { poc.OrdenCompraId, poc.ProductoId });

        modelBuilder.Entity<ProductoOrdenCompra>()
            .HasOne(poc => poc.OrdenCompra)
            .WithMany(oc => oc.ProductosOrdenCompra)
            .HasForeignKey(poc => poc.OrdenCompraId);

        modelBuilder.Entity<ProductoOrdenCompra>()
            .HasOne(poc => poc.Producto)
            .WithMany()
            .HasForeignKey(poc => poc.ProductoId);

        // Configuración para asegurar que los nombres de categoría sean únicos
        modelBuilder.Entity<Categoria>()
            .HasIndex(c => c.Nombre)
            .IsUnique();

        // Configuración para asegurar que los correos de usuario sean únicos
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Correo)
            .IsUnique();
    }
}