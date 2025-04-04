using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaParcial1._1.Data;
using TiendaParcial1._1.Models;

namespace TiendaParcial1._1.Controllers
{
    [Authorize(Roles = "Administrador,Proveedor,Usuario")] // Solo Administradores, Proveedores y Usuarios pueden acceder
    public class ProductoOrdenComprasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoOrdenComprasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductoOrdenCompras
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductosOrdenCompra.Include(p => p.OrdenCompra).Include(p => p.Producto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductoOrdenCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoOrdenCompra = await _context.ProductosOrdenCompra
                .Include(p => p.OrdenCompra)
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.OrdenCompraId == id);

            if (productoOrdenCompra == null)
            {
                return NotFound();
            }

            return View(productoOrdenCompra);
        }

        // GET: ProductoOrdenCompras/Create
        [Authorize(Roles = "Administrador,Usuario")] // Solo Administradores y Usuarios pueden crear productos en órdenes
        public IActionResult Create()
        {
            ViewData["OrdenCompraId"] = new SelectList(_context.OrdenesCompra, "Id", "Id");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre"); // Mostrar nombres en lugar de IDs
            return View();
        }

        // POST: ProductoOrdenCompras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Usuario")] // Solo Administradores y Usuarios pueden crear productos en órdenes
        public async Task<IActionResult> Create([Bind("Id,OrdenCompraId,ProductoId,Cantidad")] ProductoOrdenCompra productoOrdenCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productoOrdenCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrdenCompraId"] = new SelectList(_context.OrdenesCompra, "Id", "Id", productoOrdenCompra.OrdenCompraId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", productoOrdenCompra.ProductoId);
            return View(productoOrdenCompra);
        }

        // GET: ProductoOrdenCompras/Edit/5
        [Authorize(Roles = "Administrador,Proveedor,Usuario")] // Solo Administradores, Proveedores y Usuarios pueden editar
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoOrdenCompra = await _context.ProductosOrdenCompra.FindAsync(id);
            if (productoOrdenCompra == null)
            {
                return NotFound();
            }
            ViewData["OrdenCompraId"] = new SelectList(_context.OrdenesCompra, "Id", "Id", productoOrdenCompra.OrdenCompraId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", productoOrdenCompra.ProductoId);
            return View(productoOrdenCompra);
        }

        // POST: ProductoOrdenCompras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Proveedor,Usuario")] // Solo Administradores, Proveedores y Usuarios pueden editar
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrdenCompraId,ProductoId,Cantidad")] ProductoOrdenCompra productoOrdenCompra)
        {
            if (id != productoOrdenCompra.OrdenCompraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoOrdenCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoOrdenCompraExists(productoOrdenCompra.OrdenCompraId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrdenCompraId"] = new SelectList(_context.OrdenesCompra, "Id", "Id", productoOrdenCompra.OrdenCompraId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", productoOrdenCompra.ProductoId);
            return View(productoOrdenCompra);
        }

        // GET: ProductoOrdenCompras/Delete/5
        [Authorize(Roles = "Administrador")] // Solo Administradores pueden eliminar productos en órdenes
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoOrdenCompra = await _context.ProductosOrdenCompra
                .Include(p => p.OrdenCompra)
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.OrdenCompraId == id);

            if (productoOrdenCompra == null)
            {
                return NotFound();
            }

            return View(productoOrdenCompra);
        }

        // POST: ProductoOrdenCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")] // Solo Administradores pueden eliminar productos en órdenes
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productoOrdenCompra = await _context.ProductosOrdenCompra.FindAsync(id);
            if (productoOrdenCompra != null)
            {
                _context.ProductosOrdenCompra.Remove(productoOrdenCompra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoOrdenCompraExists(int id)
        {
            return _context.ProductosOrdenCompra.Any(e => e.OrdenCompraId == id);
        }
    }
}