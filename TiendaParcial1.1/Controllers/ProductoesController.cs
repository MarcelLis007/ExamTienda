using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaParcial1._1.Data;
using TiendaParcial1._1.Dto;
using TiendaParcial1._1.Models;

namespace TiendaParcial1._1.Controllers
{
    [Authorize(Roles = "Administrador,Proveedor")] // Solo Administradores y Proveedores pueden acceder
    public class ProductoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Productoes
        public async Task<IActionResult> Index()
        {
            try
            {
                var productos = await _context.Productos
                    .Include(p => p.Categoria)
                    .Include(p => p.Proveedor)
                    .ToListAsync();

                // Mapear los productos al DTO
                var productosDTO = productos.Select(MapToProductoDTO).ToList();
                return View(productosDTO);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error interno al cargar los productos: {ex.Message}");
            }
        }

        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var producto = await _context.Productos
                    .Include(p => p.Categoria)
                    .Include(p => p.Proveedor)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (producto == null)
                {
                    return NotFound();
                }

                // Mapear el producto al DTO
                var productoDTO = MapToProductoDTO(producto);
                return View(productoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al cargar los detalles del producto: {ex.Message}");
            }
        }

        // GET: Productoes/Create
        [Authorize(Roles = "Administrador,Proveedor")]
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre");
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "Nombre");
            return View();
        }

        // POST: Productoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Proveedor")]
        public async Task<IActionResult> Create(ProductoDTO productoDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", productoDTO.CategoriaId);
                ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "Nombre", productoDTO.ProveedorId);
                return View(productoDTO);
            }

            try
            {
                // Mapear el DTO al modelo
                var producto = MapToProducto(productoDTO);
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al crear el producto: {ex.Message}");
            }
        }

        // GET: Productoes/Edit/5
        [Authorize(Roles = "Administrador,Proveedor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                {
                    return NotFound();
                }

                // Mapear el producto al DTO
                var productoDTO = MapToProductoDTO(producto);
                ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", productoDTO.CategoriaId);
                ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "Nombre", productoDTO.ProveedorId);
                return View(productoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al cargar el formulario de edición: {ex.Message}");
            }
        }

        // POST: Productoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Proveedor")]
        public async Task<IActionResult> Edit(int id, ProductoDTO productoDTO)
        {
            if (id != productoDTO.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", productoDTO.CategoriaId);
                ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "Nombre", productoDTO.ProveedorId);
                return View(productoDTO);
            }

            try
            {
                // Mapear el DTO al modelo
                var producto = MapToProducto(productoDTO);
                _context.Update(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(productoDTO.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al actualizar el producto: {ex.Message}");
            }
        }

        // GET: Productoes/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var producto = await _context.Productos
                    .Include(p => p.Categoria)
                    .Include(p => p.Proveedor)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (producto == null)
                {
                    return NotFound();
                }

                // Mapear el producto al DTO
                var productoDTO = MapToProductoDTO(producto);
                return View(productoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al cargar el formulario de eliminación: {ex.Message}");
            }
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto != null)
                {
                    _context.Productos.Remove(producto);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al eliminar el producto: {ex.Message}");
            }
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }

        // Métodos de mapeo manual
        private ProductoDTO MapToProductoDTO(Producto producto)
        {
            return new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Cantidad = producto.Cantidad,
                CategoriaId = producto.CategoriaId,
                CategoriaNombre = producto.Categoria?.Nombre,
                ProveedorId = producto.ProveedorId,
                ProveedorNombre = producto.Proveedor?.Nombre
            };
        }

        private Producto MapToProducto(ProductoDTO productoDTO)
        {
            return new Producto
            {
                Id = productoDTO.Id,
                Nombre = productoDTO.Nombre,
                Precio = productoDTO.Precio,
                Cantidad = productoDTO.Cantidad,
                CategoriaId = productoDTO.CategoriaId,
                ProveedorId = productoDTO.ProveedorId
            };
        }
    }
}