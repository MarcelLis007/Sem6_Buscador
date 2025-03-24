using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Roles_Estructuras_Control.Data;
using Roles_Estructuras_Control.Models;
using Roles_Estructuras_Control.Models.Dto;

namespace Roles_Estructuras_Control2.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.ToListAsync());
        }



        [HttpGet]
        public async Task<IActionResult> Tabla()
        {
            // Obtener lista de productos de la BBDD
            var listaProductos = await _context.Productos
                .Select(p => new Models.Dto.DtoProducto
                {
                    Id = p.Id,
                    Nombre_Producto = p.NombreProducto,
                    Presentacion = p.Presentacion,
                    CodigoBarras = p.CodigoBarras
                })
                .ToListAsync();

            // Devuelve JSOn
            return Json(listaProductos);
        }










        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoModels = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productoModels == null)
            {
                return NotFound();
            }

            return View(productoModels);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreProducto,Presentacion,CodigoBarras")] Models.ProductoModels productoModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productoModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productoModels);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoModels = await _context.Productos.FindAsync(id);
            if (productoModels == null)
            {
                return NotFound();
            }
            return View(productoModels);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreProducto,Presentacion,CodigoBarras")] Models.ProductoModels productoModels)
        {
            if (id != productoModels.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoModelsExists(productoModels.Id))
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
            return View(productoModels);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoModels = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productoModels == null)
            {
                return NotFound();
            }

            return View(productoModels);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productoModels = await _context.Productos.FindAsync(id);
            if (productoModels != null)
            {
                _context.Productos.Remove(productoModels);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoModelsExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }

        // Añadir este método al ProductosController
        [HttpGet]
        public async Task<IActionResult> BuscarProductos(string filtro)
        {
            // Si el filtro está vacío, devuelve todos los productos
            if (string.IsNullOrEmpty(filtro))
            {
                var todosProductos = await _context.Productos
                    .Select(p => new Models.Dto.DtoProducto
                    {
                        Id = p.Id,
                        Nombre_Producto = p.NombreProducto,
                        Presentacion = p.Presentacion,
                        CodigoBarras = p.CodigoBarras
                    })
                    .ToListAsync();

                return Json(todosProductos);
            }

            // Filtrar productos por nombre (usando Contains para búsqueda parcial)
            var productosFiltrados = await _context.Productos
                .Where(p => p.NombreProducto.Contains(filtro))
                .Select(p => new Models.Dto.DtoProducto
                {
                    Id = p.Id,
                    Nombre_Producto = p.NombreProducto,
                    Presentacion = p.Presentacion,
                    CodigoBarras = p.CodigoBarras
                })
                .ToListAsync();

            return Json(productosFiltrados);
        }
    }
}
