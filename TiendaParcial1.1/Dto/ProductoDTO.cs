using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaParcial1._1.Dto
{
    public class ProductoDTO
    {
        // ID del producto (solo lectura, se genera automáticamente)
        public int Id { get; set; }

        // Nombre del producto
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; } = null!;

        // Precio del producto
        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 1000000.00, ErrorMessage = "El precio debe estar entre 0.01 y 1,000,000")]
        public decimal Precio { get; set; }

        // Cantidad disponible
        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(0, 10000, ErrorMessage = "La cantidad debe estar entre 0 y 10,000")]
        public int Cantidad { get; set; }

        // ID de la categoría
        [Required(ErrorMessage = "La categoría es obligatoria")]
        public int CategoriaId { get; set; }

        // Nombre de la categoría (solo lectura, se asigna automáticamente)
        public string? CategoriaNombre { get; set; }

        // ID del proveedor
        [Required(ErrorMessage = "El proveedor es obligatorio")]
        public int ProveedorId { get; set; }

        // Nombre del proveedor (solo lectura, se asigna automáticamente)
        public string? ProveedorNombre { get; set; }
    }
}