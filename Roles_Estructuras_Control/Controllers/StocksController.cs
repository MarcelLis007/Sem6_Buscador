using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Roles_Estructuras_Control.Data;
using Roles_Estructuras_Control.Models;

namespace Roles_Estructuras_Control.Controllers
{
    public class StocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stocks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Stocks.Include(s => s.ProductoModels).Include(s => s.ProveedoresModels);
            return View(await applicationDbContext.ToListAsync());
        }


        public bool controlarstock(int id, int cantidad) {

            var cn = _context.Stocks.FirstOrDefault(st => st.Id == id);
            if (cn == null) return false;
            if (cn.Cantidad >= cantidad) return true;
            return false;
        }



        // GET: Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockModels = await _context.Stocks
                .Include(s => s.ProductoModels)
                .Include(s => s.ProveedoresModels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockModels == null)
            {
                return NotFound();
            }

            return View(stockModels);
        }

        // GET: Stocks/Create
        public IActionResult Create()
        {
            ViewData["ProductoModelsId"] = new SelectList(_context.Productos, "Id", "NombreProducto");
            ViewData["ProveedoresModelsId"] = new SelectList(_context.Proveedores, "Id", "Correo");
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cantidad,FechaFabricacion,FechaCaducidad,FechaRegistro,ProductoModelsId,ProveedoresModelsId")] StockModels stockModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoModelsId"] = new SelectList(_context.Productos, "Id", "NombreProducto", stockModels.ProductoModelsId);
            ViewData["ProveedoresModelsId"] = new SelectList(_context.Proveedores, "Id", "Correo", stockModels.ProveedoresModelsId);
            return View(stockModels);
        }

        // GET: Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockModels = await _context.Stocks.FindAsync(id);
            if (stockModels == null)
            {
                return NotFound();
            }
            ViewData["ProductoModelsId"] = new SelectList(_context.Productos, "Id", "NombreProducto", stockModels.ProductoModelsId);
            ViewData["ProveedoresModelsId"] = new SelectList(_context.Proveedores, "Id", "Correo", stockModels.ProveedoresModelsId);
            return View(stockModels);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cantidad,FechaFabricacion,FechaCaducidad,FechaRegistro,ProductoModelsId,ProveedoresModelsId")] StockModels stockModels)
        {
            if (id != stockModels.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockModelsExists(stockModels.Id))
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
            ViewData["ProductoModelsId"] = new SelectList(_context.Productos, "Id", "NombreProducto", stockModels.ProductoModelsId);
            ViewData["ProveedoresModelsId"] = new SelectList(_context.Proveedores, "Id", "Correo", stockModels.ProveedoresModelsId);
            return View(stockModels);
        }

        // GET: Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockModels = await _context.Stocks
                .Include(s => s.ProductoModels)
                .Include(s => s.ProveedoresModels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockModels == null)
            {
                return NotFound();
            }

            return View(stockModels);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockModels = await _context.Stocks.FindAsync(id);
            if (stockModels != null)
            {
                _context.Stocks.Remove(stockModels);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockModelsExists(int id)
        {
            return _context.Stocks.Any(e => e.Id == id);
        }
    }
}
